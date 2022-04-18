using Microsoft.EntityFrameworkCore;

namespace Groceries.Repositories;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>()
            .HasKey(nameof(CartItem.OrderId), nameof(CartItem.ProductId));

        modelBuilder.Entity<Product>()
            .HasOne(pr => pr.Category)
            .WithMany(ct => ct.Products);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(ci => ci.Order);

        modelBuilder.Entity<Order>()
            .OwnsOne(o => o.CustomerInfo);
    }
}