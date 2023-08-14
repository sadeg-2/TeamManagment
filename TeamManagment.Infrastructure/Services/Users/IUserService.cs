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
        ProfileUserViewModel GetMyProfile(string userId);

        Task<string> ChangeEmail(string email,string confirmPassword,string userId);
        Task<string> ResetPassWrod(string currentpass, string newpass, string confirmpass,string userId);
    }
}
