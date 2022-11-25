using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<employees>
    {
        public void Configure(EntityTypeBuilder<employees> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Fullname).IsRequired();
            builder.Property(x => x.Address);
            builder.Property(x => x.Phone).HasMaxLength(10);
            builder.Property(x => x.Status).IsRequired();

            builder.HasOne(g => g.roles).WithMany(x => x.employees).HasForeignKey(p => p.roleID);

        }
    }
}
