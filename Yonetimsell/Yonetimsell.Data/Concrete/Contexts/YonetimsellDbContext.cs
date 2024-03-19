using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Data.Concrete.Configs;
using Yonetimsell.Data.Concrete.Extensions;
using Yonetimsell.Entity.Concrete;
using Yonetimsell.Entity.Concrete.Identity;

namespace Yonetimsell.Data.Concrete.Contexts
{
    public class YonetimsellDbContext:IdentityDbContext<User, Role, string>
    {
        public YonetimsellDbContext(DbContextOptions options): base(options) { }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<PTask> Tasks{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SeedData();
            builder.ApplyConfigurationsFromAssembly(typeof(ProjectConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(PTaskConfig).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
