using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<customers>
    {
        public void Configure(EntityTypeBuilder<customers> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Address);

            
        }
    }
}
