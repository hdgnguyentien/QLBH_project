using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace QLBH_project.Models
{
    public class CuaHangDbContext : DbContext
    {
        public CuaHangDbContext() { }
        public CuaHangDbContext(DbContextOptions<CuaHangDbContext> options) : base(options) { }
        public DbSet<cart> carts { get; set; }
        public DbSet<categories> categories { get; set; }
        public DbSet<customers> customers { get; set; }
        public DbSet<employees> employees { get; set; }
        public DbSet<orderdetails> orderdetails { get; set; }
        public DbSet<orders> orders { get; set; }
        public DbSet<productdetails> productdetails { get; set; }
        public DbSet<products> products { get; set; }
        public DbSet<roles> roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Data Source=DESKTOP-P91TD3O\\MAYAO1;Initial Catalog=QLBH_mvc;" +
               "Persist Security Info=True;User ID=tien;Password=123"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
