using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class CartConfigurations : IEntityTypeConfiguration<cart>
    {
        public void Configure(EntityTypeBuilder<cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("Decimal");
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("Decimal");

            builder.HasOne(g => g.customers).WithMany(x => x.carts).HasForeignKey(p => p.CustomerID);
            builder.HasOne(g => g.productdetails).WithMany(x => x.carts).HasForeignKey(p => p.ProductDetailID);
        }
    }
}
