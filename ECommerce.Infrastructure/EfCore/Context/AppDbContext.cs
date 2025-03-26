using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static ECommerce.Domain.Entities.Product;

namespace ECommerce.Infrastructure.EfCore.Context;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = DESKTOP-ZAKIR; Database = ECommerceProject;Trusted_Connection =True;TrustServerCertificate=True");
    }
}
