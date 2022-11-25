using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<products>
    {
        public void Configure(EntityTypeBuilder<products> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).HasColumnType("bit").IsRequired();
        }
    }
}
