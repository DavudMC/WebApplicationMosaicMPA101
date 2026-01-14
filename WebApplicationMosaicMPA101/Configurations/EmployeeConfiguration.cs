using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationMosaicMPA101.Models;

namespace WebApplicationMosaicMPA101.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(1024);
            builder.Property(x => x.PositionId).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1024);

            builder.HasOne(x => x.Position).WithMany(x => x.Employees).HasForeignKey(x => x.PositionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
