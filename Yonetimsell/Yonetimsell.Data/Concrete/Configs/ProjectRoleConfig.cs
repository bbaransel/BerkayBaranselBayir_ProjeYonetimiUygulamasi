using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Configs
{
    public class ProjectRoleConfig : IEntityTypeConfiguration<ProjectRole>
    {
        public void Configure(EntityTypeBuilder<ProjectRole> builder)
        {
            builder.HasKey(x=> new {x.UserId, x.ProjectId});
            builder.ToTable("ProjectRoles");
        }
    }
}
