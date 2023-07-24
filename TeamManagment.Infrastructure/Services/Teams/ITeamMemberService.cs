using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagment.Core.Dtos.Assignments;
using MyTeam = TeamManagment.Core.Helper.Teams;

namespace TeamManagment.Infrastructure.Services.Teams
{
    public interface ITeamMemberService
    {
        Task<TeamMember> CreateAsync(CreateMemberDto dto, string username);
        Task<TeamMember> UpdateAsync(UpdateMemberDto dto, string username);
        int DeleteAsync(int memberId ,string teamLeaderUserName);
        UpdateMemberDto GetAsync(int memberID);
        Task<Response<TeamMemberViewModel>> GetAllForDataTable(Request request, int teamId);
        Task<Response<AssignmentViewModel>> GetAllAssignmentData(Request request, int teamId , string userId);

        int AssignTask(CreateAssignmentsDto dto, string CreatorId);

        ProfileTeamMemberViewModel GetMyProfile(string memberId);

        AssignmentViewModel GetAssignment(int id);
        List<MyTeam> GetMyTeam(string memberId);

    }
}
