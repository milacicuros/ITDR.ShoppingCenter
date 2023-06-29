using ITDR.ShoppingCenter.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ITDR.ShoppingCenter.Services.CouponAPI.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 1,
            CouponCode = "100OFF",
            DiscountAmount = 100
        });
        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 2,
            CouponCode = "200OFF",
            DiscountAmount = 200
        });
        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 3,
            CouponCode = "250OFF",
            DiscountAmount = 250
        });
    }
}