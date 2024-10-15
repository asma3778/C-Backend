using Microsoft.EntityFrameworkCore;
using sda_3_online_Backend_Teamwork.src.Entity;

namespace sda_3_online_Backend_Teamwork.src.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public DataBaseContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Save enum as number in PostgreSQL
            modelBuilder.HasPostgresEnum<Role>();

            // Remove explicit relationship configuration if conventions are followed.

            // The following Fluent API configurations are no longer necessary:
            // EF Core conventions will automatically configure these relationships.

            /*
             modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

             modelBuilder.Entity<Brand>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Brands)
                .HasForeignKey(b => b.CategoryId);

             modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
            */
        }
    }
}
