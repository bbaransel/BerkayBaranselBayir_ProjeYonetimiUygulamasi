using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Data.Concrete.Configs;
using Yonetimsell.Data.Concrete.Extensions;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;

namespace Yonetimsell.Data.Concrete.Contexts
{
    public class YonetimsellDbContext : IdentityDbContext<User, Role, string>
    {
        public YonetimsellDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<PTask> PTasks { get; set; }
        public DbSet<Subscription> Subscriptions{ get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SeedData();
            builder.ApplyConfigurationsFromAssembly(typeof(ProjectConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(PTaskConfig).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
