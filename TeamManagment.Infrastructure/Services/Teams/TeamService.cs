﻿using System.Linq.Dynamic.Core;
using TeamManagment.Core.Enums;

namespace TeamManagment.Infrastructure.Services.Teams
{
    public class TeamService : ITeamService
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public TeamService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<Team> CreateAsync(CreateTeamDto dto, string teamLeaderUserName)
        {
            // Confirm if user exist
            var userLeader = _db.Users.SingleOrDefault(x => x.UserName == teamLeaderUserName);

            if (userLeader == null)
            {
                throw new Exception();
            }
            // Team Creation
            var team = _mapper.Map<Team>(dto);
            team.CreatedAt = DateTime.Now;
            team.CreatedBy = teamLeaderUserName;
            team.TeamLeaderUserName = teamLeaderUserName;
            team.NumOfTeamMember = 1;
            if (dto.ImageUrl != null)
            {
                team.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }
            await _db.Teams.AddAsync(team);
            _db.SaveChanges();
            // Add Tead Leader
            var teamLeader = new TeamMember()
            {
                MemberPosition = TeamUserType.TeamLeader,
                MemberId = userLeader.Id,
                CreatedAt = DateTime.Now,
                CreatedBy = teamLeaderUserName,
                TeamId = team.Id,
            };
            _db.TeamMembers.Add(teamLeader);
            _db.SaveChanges();

            return team;
        }

        public int DeleteAsync(int teamId)
        {
            var team = _db.Teams.SingleOrDefault(x => !x.IsDelete && x.Id == teamId);
            if (team == null)
            {
                throw new Exception();
            }
            team.IsDelete = true;

            _db.Update(team);
            _db.SaveChanges();

            return team.Id;
        }

        public async Task<Response<TeamViewModel>> GetAllForDataTable(Request request)
        {
            Response<TeamViewModel> response = new Response<TeamViewModel>() { Draw = request.Draw };

            var data = _db.Teams.AsQueryable();
            response.RecordsTotal = data.Count();

            if (request.Search.Value != null)
            {
                data = data.Where(x =>
                    x.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
                    x.Description.ToLower().Contains(request.Search.Value.ToLower()) ||
                    x.TeamLeaderUserName.ToLower().Contains(request.Search.Value.ToLower())
                );
            }

            response.RecordsFiltered = await data.CountAsync();

            if (request.Order != null && request.Order.Count > 0)
            {
                var sortColumn = request.Columns.ElementAt(request.Order.FirstOrDefault().Column).Name;
                var sortDirection = request.Order.FirstOrDefault().Dir;
                data = data.OrderBy(string.Concat(sortColumn, " ", sortDirection));
            }
            response.Data = await data.Skip(request.Start).Take(request.Length).
                Select(x => new TeamViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    NumOfTeamMember = x.NumOfTeamMember,
                    TeamLeaderUserName = x.TeamLeaderUserName,
                    IsDelete = x.IsDelete,
                }).ToListAsync();
            return response;
        }

        public UpdateTeamDto GetAsync(int teamId)
        {
            var team = _db.Teams.SingleOrDefault(x => x.Id == teamId);
            if (team == null)
            {
                throw new Exception();
            }
            return _mapper.Map<UpdateTeamDto>(team);
        }

        public TeamViewModel GetTeam(int teamId) {
            var team = _db.Teams.SingleOrDefault(x => x.Id == teamId);
            if (team == null)
            {
                throw new Exception();
            }
            return _mapper.Map<TeamViewModel>(team);

        }
        public async Task<Team> UpdateAsync(UpdateTeamDto dto, string username)
        {
            var team =  _db.Teams.SingleOrDefault(x => x.Id == dto.Id);
            if (team == null)
            {
                throw new Exception();
            }
            if (dto.ImageUrl != null)
            {
                team.ImageUrl = await _fileService.SaveFile(dto.ImageUrl, FolderNames.ImagesFolder);
            }
            var updatedUser = _mapper.Map(source: dto, destination: team);
            _db.Update(updatedUser);
            _db.SaveChanges();
            return team;

        }
    }
}
