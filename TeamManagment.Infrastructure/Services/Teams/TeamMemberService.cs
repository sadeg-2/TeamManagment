using System.Linq.Dynamic.Core;

namespace TeamManagment.Infrastructure.Services.Teams
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ApplicationDbContext _db;
        public TeamMemberService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<TeamMember> CreateAsync(CreateMemberDto dto, string teamLeaderUserName /*team leader*/)
        {
            var member = await _db.Users.SingleOrDefaultAsync(x => x.UserName == dto.MemberUserName && !x.IsDelete);
            if (member == null)
            {
                throw new Exception();
            }
            var team = _db.Teams.SingleOrDefault(x => x.Id == dto.TeamId && !x.IsDelete && x.TeamLeaderUserName == teamLeaderUserName);
            if (team == null)
            {
                throw new Exception();
            }
            var isAdded = _db.TeamMembers.Any(x => x.TeamId == team.Id && x.MemberId == member.Id);
            if (isAdded) {
                throw new Exception();
            }
            var newMember = new TeamMember
            {
                MemberId = member.Id,
                CreatedAt = DateTime.Now,
                MemberPosition = dto.MemberPosition,
                TeamId = dto.TeamId,
                CreatedBy = teamLeaderUserName,
                UserName = dto.MemberUserName,
            };
            team.NumOfTeamMember++;

            _db.Add(newMember);
            _db.Update(team);
            _db.SaveChanges();
            return newMember;
        }

        public int DeleteAsync(int memberId , string teamLeaderUserName)
        {
            var member = _db.TeamMembers.SingleOrDefault(x => !x.IsDelete && x.Id == memberId);
            if (member == null)
            {
                throw new Exception();
            }
            var team = _db.Teams.SingleOrDefault(x => !x.IsDelete && x.TeamLeaderUserName == teamLeaderUserName);
            if (team == null)
            {
                throw new Exception();
            }
            member.IsDelete = true;
            team.NumOfTeamMember--;
            _db.Update(member);
            _db.Update(team);
            _db.SaveChanges();

            throw new NotImplementedException();
        }

        public async Task<Response<TeamMemberViewModel>> GetAllForDataTable(Request request , int teamId)
        {
            Response<TeamMemberViewModel> response = new Response<TeamMemberViewModel>() { Draw = request.Draw };

            var data = _db.TeamMembers.Where(x=> x.TeamId == teamId).AsQueryable();
            response.RecordsTotal = data.Count();

            if (request.Search.Value != null)
            {
                data = data.Where(
                    x =>
                    x.UserName.ToLower().Contains(request.Search.Value.ToLower())
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
                Select(x => new TeamMemberViewModel
                {
                    Id = x.Id,
                    JoinDate = x.CreatedAt.ToShortDateString(),
                    MemberPosition = x.MemberPosition.ToString(),
                    UserName = x.UserName,
                    
                }).ToListAsync();
            return response;
        }

        public UpdateMemberDto GetAsync(int memberID)
        {
            throw new NotImplementedException();
        }

        public Task<TeamMember> UpdateAsync(UpdateMemberDto dto, string username)
        {
            throw new NotImplementedException();
        }
    }
}
