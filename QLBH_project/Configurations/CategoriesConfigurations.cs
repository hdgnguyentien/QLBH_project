using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLBH_project.Models;

namespace QLBH_project.Configurations
{
    public class CategoriesConfigurations : IEntityTypeConfiguration<categories>
    {
        public void Configure(EntityTypeBuilder<categories> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
