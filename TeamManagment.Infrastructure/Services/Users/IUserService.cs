
namespace TeamManagment.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<User> CreateAsync(CreateUserDto dto);
        Task<string> UpdateAsync(UpdateUserDto dto);
        Task<string> DeleteAsync(string id);
        Task<UpdateUserDto> GetAsync(string id);
        Task<Response<UserViewModel>> GetAllForDataTable(Request request);
        User GetUser(string id);
    }
}
