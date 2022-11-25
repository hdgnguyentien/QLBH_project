using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class OrderDetailConfigurations : IEntityTypeConfiguration<orderdetails>
    {
        public void Configure(EntityTypeBuilder<orderdetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("Decimal");
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("Decimal");

            builder.HasOne(g => g.productdetails).WithMany(x => x.orderdetails).HasForeignKey(p => p.ProductDetailID);
            builder.HasOne(g => g.orders).WithMany(x => x.orderdetails).HasForeignKey(p => p.OrderId);
        }
    }
}
