using System.Linq.Dynamic.Core;
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
        public async Task<User> CreateAsync(CreateUserDto dto)
        {
            //var idExist = await _db.Users.AnyAsync(x => !x.IsDelete);
            //if (idExist)
            //{
            //    throw new Exception("");
            //}
            var user = _mapper.Map<User>(dto);
            user.UserName = dto.Email;
            if (dto.ImageUrl != null)
            {
                user.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }
            try
            {
                var result = await _userManager.CreateAsync(user, "Sadeg$2001");
            }
            catch (Exception) { 
            
            }
           
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
            response.Data = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return response;

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
            var user = await _db.Users.FindAsync(dto.Id);
            if (user == null)
            {
                throw new Exception();
            }
            if (dto.ImageUrl != null)
            {
                user.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }

            var updatedUser = _mapper.Map(source : dto,destination :user);
            _db.Update(updatedUser);
            _db.SaveChanges();
            return user.Id;
        }
        public User GetUser(string id) {
            return _db.Users.Single(x => x.Id == id);
        }
    }
}
