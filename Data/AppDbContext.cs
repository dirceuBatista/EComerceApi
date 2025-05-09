using EComerceApi.Data.Mapping;
using EComerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EComerceApi.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new OrderItemMap());
        modelBuilder.ApplyConfiguration(new CustomerMap());
        

        
    }


    protected override void OnConfiguring(DbContextOptionsBuilder Builder)
    {
        var conection = Builder.UseSqlServer
            ("server=localhost,1433; DataBase=EComerceApi;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;");
        
    }
    
}