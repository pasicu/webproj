using Common.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {
        }

        public virtual DbSet<UserModel> Users { get; set; } = default!;
        public virtual DbSet<Product> Products { get; set; } = default!;
        public virtual DbSet<Order> Orders { get; set; } = default!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            base.OnModelCreating(modelBuilder);
        }
    }
}