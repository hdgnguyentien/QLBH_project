using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class ProductDetailConfigurations : IEntityTypeConfiguration<productdetails>
    {
        public void Configure(EntityTypeBuilder<productdetails> builder)
        {
            builder.ToTable("ProductDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Price).HasColumnType("Decimal").IsRequired();
            builder.Property(x => x.OriginalPrice).HasColumnType("Decimal").IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired().HasColumnType("DateTime");
            builder.Property(x => x.LinkImage);
            builder.Property(x => x.Status).IsRequired().HasColumnType("bit");

            builder.HasOne(g => g.products).WithMany(x => x.productdetails).HasForeignKey(p => p.ProductId);
            builder.HasOne(g => g.categories).WithMany(x => x.productdetails).HasForeignKey(p => p.CategoriesID);
        }
    }
}
