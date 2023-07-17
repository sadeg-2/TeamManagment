using Microsoft.Extensions.DependencyInjection;

using TeamManagment.Infrasrtucture.AutoMapper;
using TeamManagment.Infrastructure.Services;
using TeamManagment.Infrastructure.Services.Tasks;
using TeamManagment.Infrastructure.Services.Teams;
using TeamManagment.Infrastructure.Services.Users;

namespace TeamManagment.Infrastructure.Extensions
{
    public static class ServiceContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService,FileService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITeamService, TeamService>();

            return services;
        }
    }
}
