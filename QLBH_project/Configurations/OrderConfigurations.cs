using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<orders>
    {
        public void Configure(EntityTypeBuilder<orders> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreate).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.TotalPrice).HasColumnType("Decimal").IsRequired();
            builder.Property(x => x.Status).HasColumnType("bit").IsRequired();

            builder.HasOne(g => g.employees).WithMany(x => x.orders).HasForeignKey(p => p.EmployeeId);
            builder.HasOne(g => g.customers).WithMany(x => x.orders).HasForeignKey(p => p.CustomerId);
        }
    }
}
