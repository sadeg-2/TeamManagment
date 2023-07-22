using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Infrastructure.Services.Teams
{
    public interface ITeamMemberService
    {
        Task<TeamMember> CreateAsync(CreateMemberDto dto, string username);
        Task<TeamMember> UpdateAsync(UpdateMemberDto dto, string username);
        int DeleteAsync(int memberId ,string teamLeaderUserName);
        UpdateMemberDto GetAsync(int memberID);
        Task<Response<TeamMemberViewModel>> GetAllForDataTable(Request request, int teamId);


    }
}
