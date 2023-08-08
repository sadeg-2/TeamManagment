using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamManagment.Data.Models;
using MyTask = TeamManagment.Data.Models.Task;

namespace TeamManagment.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Attatchment> Attatchments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<User>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<TeamMember>().HasQueryFilter(x => !x.Team.IsDelete);
        }

    }
    
}