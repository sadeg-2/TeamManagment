using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TeamManagment.Infrasrtucture.AutoMapper;
using TeamManagment.Infrastructure.Services;
using TeamManagment.Infrastructure.Services.Comments;
using TeamManagment.Infrastructure.Services.HangFireConfig;
using TeamManagment.Infrastructure.Services.Reviews;
using TeamManagment.Infrastructure.Services.Submissions;
using TeamManagment.Infrastructure.Services.Tasks;
using TeamManagment.Infrastructure.Services.Teams;
using TeamManagment.Infrastructure.Services.Users;

namespace TeamManagment.Infrastructure.Extensions
{
    public static class ServiceContainer
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFileService,FileService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ISubmissionService, SubmissionService>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                // Set the desired session timeout if needed
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHangfireServer();

            return builder;
        }

        public static WebApplication BuildHangFire(this WebApplication app) {
            app.UseHangfireDashboard("/dashborad" , new DashboardOptions {

                Authorization =(new[] { new HangfireDashboardAuthorizationFilter() })
            });

            return app;
        }
    }
}
