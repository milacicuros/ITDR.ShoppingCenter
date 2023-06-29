using ITDR.ShoppingCenter.Services.CouponAPI.DTOs;

namespace ITDR.ShoppingCenter.Services.CouponAPI.Repository;

public interface ICouponRepository
{
    Task<CouponDTO> GetCouponByCodeAsync(string couponCode);
}