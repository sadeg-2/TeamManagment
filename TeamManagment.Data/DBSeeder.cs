using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using TeamManagment.Core.Enums;
using TeamManagment.Data.Models;
using TeamManagment.Web.Data;
using Task = System.Threading.Tasks.Task;

namespace TeamManagment.Data
{
    public static class DBSeeder
	{
        public static IHost SeedDb(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                roleManager.SeedRoles().Wait();
                userManager.SeedUser().Wait();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return webHost;
        }

       
        public static async Task SeedUser(this UserManager<User> userManger)
        {
            if (await userManger.Users.AnyAsync())
            {
                return;
            }
            var user = new User();
            user.FullName = "System Developer";
            user.UserName = "dev@gmail.com";
            user.Email = "dev@gmail.com";
            user.DOB = DateTime.Now;
            user.PhoneNumber = "00000000";
            user.ImageUrl = "300-1.jpg";
            user.CreatedAt = DateTime.Now;
            await userManger.CreateAsync(user, "Admin111$$");
            await userManger.AddToRoleAsync(user, UserType.Adminstrator.ToString());

        }

        public static async Task SeedRoles(this RoleManager<IdentityRole> roleManager) {
            if (await roleManager.Roles.AnyAsync())
            {
                return;
            }
            var roles = new List<string>();
            foreach (var type in Enum.GetValues(typeof(UserType)))
            {
                roles.Add(type.ToString());
            }
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

        }
    }
}
