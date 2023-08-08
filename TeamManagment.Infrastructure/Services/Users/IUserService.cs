
using TeamManagment.Core.Enums;

namespace TeamManagment.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<User> CreateAsync(CreateUserDto dto , string password);
        Task<string> UpdateAsync(UpdateUserDto dto);
        Task<string> DeleteAsync(string id);
        string Recover(string id);
        Task<UpdateUserDto> GetAsync(string id);
        Task<Response<UserViewModel>> GetAllForDataTable(Request request);
        User GetUser(string id);
        Task<List<UserType>> GetUserRole(string id);
    }
}
