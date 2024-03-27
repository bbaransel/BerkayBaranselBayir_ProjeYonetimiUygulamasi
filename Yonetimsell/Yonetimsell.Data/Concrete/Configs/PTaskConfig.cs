using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yonetimsell.Entity.Concrete;

namespace Yonetimsell.Data.Concrete.Configs
{
    public class PTaskConfig : IEntityTypeConfiguration<PTask>
    {
        public void Configure(EntityTypeBuilder<PTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
            builder.ToTable("PTasks");

        }
    }
}
