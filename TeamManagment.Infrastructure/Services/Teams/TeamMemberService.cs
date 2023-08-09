using System.Linq.Dynamic.Core;
using TeamManagment.Core.Dtos.Assignments;
using TeamManagment.Core.Dtos.Notifications;
using TeamManagment.Core.Enums;
using TeamManagment.Infrastructure.Services.Notifications;
using MyTeam = TeamManagment.Core.Helper.Teams;

namespace TeamManagment.Infrastructure.Services.Teams
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ApplicationDbContext _db;
        private readonly INotificationService _notificationService;
        public TeamMemberService(ApplicationDbContext db , INotificationService notificationService)
        {
            _db = db;
            _notificationService = notificationService;
        }

        public async Task<int> AssignTask(CreateAssignmentsDto dto, string CreatorId)
        {
            var member = _db.TeamMembers.SingleOrDefault(x => !x.IsDelete && x.Id == dto.MemberId);
            if (member == null)
            {
                throw new Exception();
            }
            var teamLeader = _db.TeamMembers.SingleOrDefault(x =>
                            x.TeamId == member.TeamId &&
                            x.MemberPosition == TeamUserType.TeamLeader &&
                            x.MemberId == CreatorId 
                            );
            if (teamLeader == null)
            {
                throw new Exception();
            }

            var task = new MyTask
            {
                AssigneeId = member.MemberId,
                CreatedAt = DateTime.Now,
                CreatedBy = teamLeader.UserName,
                DeadLine = dto.DeadLine,
                Description = dto.Description ,
                Title = dto.Title ,
                IsCompleted = TaskStatee.UnCompleted,
                relatedWithAssignment = true ,

            };
            _db.Add(task);
            _db.SaveChanges();

            var assignment = new Assignment
            {
                TaskId = task.Id,
                TeamId = member.TeamId,
                CreatorId = teamLeader.Id,
                CreatedBy = teamLeader.UserName
            };
            _db.Add(assignment);
            _db.SaveChanges();
            var notify = new NotificationDto
            {
                Action = NotificationAction.general,
                Message = "There is an hour left until the deadline for submitting the task",
                Title = task.Title,
                UserId = task.AssigneeId,
                SendAt = task.DeadLine - TimeSpan.FromHours(1),
            };
            await _notificationService.AddNotify(notify);

            return assignment.Id;
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
            var isAdded = _db.TeamMembers.Any(x => x.TeamId == team.Id && x.MemberId == member.Id && !x.IsDelete);
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

        public int Delete(int memberId , string teamLeaderUserName , int teamId)
        {
            var member = _db.TeamMembers.SingleOrDefault(x => !x.IsDelete && x.Id == memberId);
            if (member == null)
            {
                throw new Exception();
            }
            var team = _db.Teams.SingleOrDefault(x => !x.IsDelete && x.TeamLeaderUserName == teamLeaderUserName && x.Id == teamId);
            if (team == null)
            {
                throw new Exception();
            }
            member.IsDelete = true;
            team.NumOfTeamMember--;
            _db.Update(member);
            _db.Update(team);
            _db.SaveChanges();

            return member.Id;
        }

        public async Task<Response<AssignmentViewModel>> GetAllAssignmentData(Request request, int teamId, string userId)
        {
            Response<AssignmentViewModel> response = new Response<AssignmentViewModel>() { Draw = request.Draw };

            var data = _db.Assignments.Include(x=> x.Task).Include(x=> x.Team).Where(x => (x.TeamId == teamId || teamId == 0)&&(!x.Team.IsDelete)).AsQueryable();
            response.RecordsTotal = data.Count();

            if (request.Search.Value != null)
            {
                data = data.Where(
                    x =>
                    x.Task.Title.ToLower().Contains(request.Search.Value.ToLower())||
                    x.Team.Name.ToLower().Contains(request.Search.Value.ToLower())
                );
            }
            response.RecordsFiltered = await data.CountAsync();

            //if (request.Order != null && request.Order.Count > 0)
            //{
            //    var sortColumn = request.Columns.ElementAt(request.Order.FirstOrDefault().Column).Name;
            //    var sortDirection = request.Order.FirstOrDefault().Dir;
            //    data = data.OrderBy(string.Concat(sortColumn, " ", sortDirection));
            //}
            response.Data = await data.Skip(request.Start).Take(request.Length).
                Select(x => new AssignmentViewModel
                {
                    Id = x.Id,
                    DeadLine = x.Task.DeadLine.ToShortDateString(),
                    Status = x.Task.IsCompleted.ToString(),
                    TeamName = x.Team.Name ,
                    Title = x.Task.Title ,
                }).ToListAsync();
            return response;
        }

        public async Task<Response<TeamMemberViewModel>> GetAllForDataTable(Request request , int teamId)
        {
            Response<TeamMemberViewModel> response = new Response<TeamMemberViewModel>() { Draw = request.Draw };

            var data = _db.TeamMembers.Where(x=> x.TeamId == teamId && !x.IsDelete && !x.Member.IsDelete  ).AsQueryable();
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
                    MemberId = x.MemberId,
                    
                }).ToListAsync();
            return response;
        }

        public AssignmentViewModel GetAssignment(int id)
        {
            var assignment = _db.Assignments.Include(x=> x.Task).Include(x=> x.Team).SingleOrDefault(x => !x.IsDelete && x.Id == id);
            if (assignment == null)
            {
                throw new Exception();
            }
            var assignmentVm = new AssignmentViewModel { 
                DeadLine = assignment.Task.DeadLine.ToLongDateString(),
                Description = assignment.Task.Description ,
                Id = assignment.Id ,
                Status = assignment.Task.IsCompleted.ToString(),
                Title = assignment.Task.Title ,
                TeamName = assignment.Team.Name
            };

            return assignmentVm;
        }

        public UpdateMemberDto GetAsync(int memberID)
        {
            throw new NotImplementedException();
        }

        public ProfileTeamMemberViewModel GetMemberProfile(string memberId)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == memberId);
            var numOfTaskDone = _db.Assignments.Include(x => x.Task)
                .Count(x=> !x.IsDelete && x.Task.AssigneeId == memberId &&
                            x.Task.IsCompleted == TaskStatee.IsCompleted);
            var numOfTeamJoined = _db.TeamMembers.Include(x=> x.Team).Count(x=> !x.IsDelete && !x.Team.IsDelete && x.MemberId == memberId);
            var profile = new ProfileTeamMemberViewModel
            {
                FullName = user.FullName,
                Id = user.Id,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                NumOfTaskDone = numOfTaskDone,
                NumOfTeamJoined = numOfTeamJoined,
            };

            return profile;
        }

        public List<MyTeam> GetMyTeam(string memberId)
        {
            if (memberId == null) {
                throw new Exception();
            }
            var teams = _db.TeamMembers.Include(x => x.Team).Where(x => x.MemberId == memberId && !x.IsDelete && !x.Team.IsDelete).
                Select(x => new MyTeam { 
                        Id = x.TeamId,
                        Name = x.Team.Name,
                }).ToList();
            return teams;
        }

        public Task<TeamMember> UpdateAsync(UpdateMemberDto dto, string username)
        {
            throw new NotImplementedException();
        }
        


    
    }
}
