using ITDR.ShoppingCenter.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ITDR.ShoppingCenter.Services.ProductAPI.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 1,
            Name = "Glow In The Dark",
            Price = 750.00,
            Description = "Upravljanje karakterom i pokusaj da se nadje izlaz iz lavirinta.",
            ImageURL = "https://localhost:7279/images/glow-in-the-dark.jpg",
            CategoryName = "Misterije"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 2,
            Name = "Call Of Duty - Modern Warfare II",
            Price = 1250.00,
            Description = "Call Of Duty MWII je najprodavanija ratna igrica u kojoj se resavaju misije iz doba 2010.",
            ImageURL = "https://localhost:7279/images/cod-mw-2.jpg",
            CategoryName = "Akcija, Ratne"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 3,
            Name = "God Of War",
            Price = 1100.00,
            Description = "U ulozi Kratosa prelazite teske misije.",
            ImageURL = "https://localhost:7279/images/god-of-war.jpg",
            CategoryName = "Akcija"
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            ProductId = 4,
            Name = "Witcher",
            Price = 1350.00,
            Description = "Witcher, bestseler!",
            ImageURL = "https://localhost:7279/images/witcher.jpg",
            CategoryName = "Akcija, Misterija"
        });
    }
}