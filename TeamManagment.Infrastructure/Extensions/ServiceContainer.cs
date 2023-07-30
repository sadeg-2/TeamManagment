using Microsoft.Extensions.DependencyInjection;

using TeamManagment.Infrasrtucture.AutoMapper;
using TeamManagment.Infrastructure.Services;
using TeamManagment.Infrastructure.Services.Comments;
using TeamManagment.Infrastructure.Services.Reviews;
using TeamManagment.Infrastructure.Services.Submissions;
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
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ISubmissionService, SubmissionService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set the desired session timeout if needed
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            return services;
        }
    }
}
