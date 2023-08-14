using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamManagment.Data;
using TeamManagment.Data.Models;
using TeamManagment.Infrastructure.Extensions;
using TeamManagment.Infrastructure.Hubs;
using TeamManagment.Infrastructure.Middlewares;
using TeamManagment.Infrastructure.Services.Authentications;
using TeamManagment.Web.Data;
using TeamManagment.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddControllersWithViews();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilter));
});

builder.Services.AddIdentity<User, IdentityRole>(
                config => {
                    config.SignIn.RequireConfirmedAccount = false;
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequireDigit = false;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.SignIn.RequireConfirmedEmail = false;

                })
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultUI()
            .AddSignInManager<CustomSignInManager>();

builder.RegisterServices();
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.BuildHangFire();
app.MapHub<ChatHub>("/chatHub");
app.MapHub<NotifyHub>("/NotifyHub");
app.UseMiddleware<NotFoundMiddleware>();
//app.UseMiddleware<ExceptionMiddleware>();



app.SeedDb().Run();
