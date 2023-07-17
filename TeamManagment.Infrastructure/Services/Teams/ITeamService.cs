

using TeamManagment.Core.Enums;

namespace TeamManagment.Infrastructure.Services.Teams
{
    public interface ITeamService
    {
        Task<Team> CreateAsync(CreateTeamDto dto , string username);
        Task<Team> UpdateAsync(UpdateTeamDto dto, string username);
        int DeleteAsync(int teamId);
        UpdateTeamDto GetAsync(int teamId);
        Task<Response<TeamViewModel>> GetAllForDataTable(Request request);


    }
}
