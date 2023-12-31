﻿using AutoMapper.Execution;
using System.Linq.Dynamic.Core;
using TeamManagment.Core.Enums;

namespace TeamManagment.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly UserManager<User> _userManager;
        public UserService(
                ApplicationDbContext db,
                IMapper mapper,
                UserManager<User> userManager,
                IFileService fileService
                )
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _fileService = fileService;
        }
        public async Task<User> CreateAsync(CreateUserDto dto , string password)
        {
            //var idExist = await _db.Users.AnyAsync(x => !x.IsDelete);
            //if (idExist)
            //{
            //    throw new Exception("");
            //}

            var user = _mapper.Map<User>(dto);
            user.UserName = dto.Email;
            if (dto.ImageUrl != null )
            {
                user.ImageUrl = await _fileService.SaveImage(dto.ImageUrl, FolderNames.ImagesFolder);
            }
            else {
                throw new Exception();
            }
            user.CreatedAt = DateTime.Now;
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, UserType.AnyOne.ToString());
            return user;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x=> x.Id == id);
            if (user == null)
            {
                throw new Exception();
            }
            user.IsDelete = true;
            _db.Update(user);
            await _db.SaveChangesAsync();
            return user.Id;
        } 
        public string Recover(string id)
        {
            var user = _db.Users.SingleOrDefault(x=> x.Id == id);
            if (user == null)
            {
                throw new Exception();
            }
            user.IsDelete = false;
            _db.Update(user);
            _db.SaveChanges();
            return user.Id;
        }

        public async Task<Response<UserViewModel>> GetAllForDataTable(Request request)
        {
            var c = request;
            Response<UserViewModel> response = new Response<UserViewModel>() { Draw = request.Draw };

            var data = _db.Users.AsQueryable();

            response.RecordsTotal = data.Count();

            if (request.Search.Value != null)
            {
                data = data.Where(x =>
                    string.IsNullOrEmpty(request.Search.Value) ||
                    x.FullName.ToLower().Contains(request.Search.Value.ToLower()) ||
                    x.Email.ToLower().Contains(request.Search.Value.ToLower())||
                    x.PhoneNumber.ToLower().Contains(request.Search.Value.ToLower())
                );
            }
            response.RecordsFiltered = await data.CountAsync();

            if (request.Order != null && request.Order.Count > 0)
            {
                var sortColumn = request.Columns.ElementAt(request.Order.FirstOrDefault().Column).Name;
                var sortDirection = request.Order.FirstOrDefault().Dir;
                data = data.OrderBy(string.Concat(sortColumn," ",sortDirection));
            }
            var dataList = await data.Skip(c.Start).Take(c.Length).ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(dataList);
            foreach (var user in users)
            {
                user.roles = await GetUserRoleString(user.Id);
            }
            response.Data = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return response;

        }
        public async Task<string> GetUserRoleString(string id)
        {
            var userTypesList = await GetUserRole(id);
            var userTypesString = string.Join(", ", userTypesList.Select(ut => ut.ToString()));
            return userTypesString;
        }

        public async Task<UpdateUserDto> GetAsync(string id)
        {
            var student = await _db.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                throw new Exception("");
            }
            return _mapper.Map<UpdateUserDto>(student);
        }

        public async Task<string> UpdateAsync(UpdateUserDto dto)
        {
            var idExist = await _db.Users.AnyAsync(x => x.Id == dto.Id );
            if (!idExist)
            {
                throw new Exception();
            }
            try
            {
                var user = await _db.Users.FindAsync(dto.Id);
                if (user == null)
                {
                    throw new Exception();
                }
                if (dto.ImageUrl != null)
                {
                    user.ImageUrl = await _fileService.SaveImage(dto.ImageUrl, FolderNames.ImagesFolder);
                }

                var updatedUser = _mapper.Map(source: dto, destination: user);
                _db.Update(updatedUser);
                _db.SaveChanges();
            }
            catch (Exception) { 
            }
            return dto.Id;
        }
        public User GetUser(string id) {
            return _db.Users.Single(x => x.Id == id);
        }
        public async Task<List<UserType>> GetUserRole(string id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == id);
            var roles = await _userManager.GetRolesAsync(user);

            var userTypes = new List<UserType>();
            
            foreach (var role in roles)
            {
                if (Enum.TryParse(role, out UserType userType))
                {
                    userTypes.Add(userType);
                }
            }
            return userTypes;
        }

        public ProfileUserViewModel GetMyProfile(string userId)
        {
            var user = _db.Users.SingleOrDefault(x => !x.IsDelete && x.Id == userId);
            if (user == null)
            {
                throw new Exception();
            }
            var numOfTeamJoined = _db.TeamMembers.Include(x => x.Team).Count(x => !x.IsDelete && !x.Team.IsDelete && x.MemberId == userId);
            var query = _db.Reviews.Where(u => userId == u.ReciverId && !u.IsDelete);
            var rating = 0;
            if (query.Any())
            {
                rating =(int) query.Average(x => x.Rating);
            }

            var profileUserViewModel = new ProfileUserViewModel { 
               Email = user.Email,
               FullName = user.FullName,
               Id = user.Id,
               ImageUrl = user.ImageUrl,
               NumOfTeamJoined = numOfTeamJoined,
               Rating = rating,
               DOB = user.DOB,
               PhoneNumber=user.PhoneNumber,
            };
            return profileUserViewModel; 
        }

        public async Task<string> ChangeEmail(string email, string confirmPassword, string userId) {
            var user = _db.Users.SingleOrDefault(x=> x.Id == userId);
            if (user== null)
            {
                throw new Exception();
            }
            var isValidPassword =  await _userManager.CheckPasswordAsync(user, confirmPassword);
            if (!isValidPassword) {
                throw new Exception();
            }
            user.Email = email;
            _db.Update(user);
            _db.SaveChanges();
            return userId;
        }
        public async Task<string> ResetPassWrod(string currentpass, string newpass, string confirmpass, string userId) {
            var user = _db.Users.SingleOrDefault(x=> x.Id == userId);
            if (user == null || confirmpass != newpass)
            {
                throw new Exception();
            }
            await _userManager.ChangePasswordAsync(user, currentpass, newpass);

            return userId;
        }
    }
}
