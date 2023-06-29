using AutoMapper;
using ITDR.ShoppingCenter.Services.CouponAPI.DbContexts;
using ITDR.ShoppingCenter.Services.CouponAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ITDR.ShoppingCenter.Services.CouponAPI.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _db;
    protected IMapper _mapper;
    
    public CouponRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<CouponDTO> GetCouponByCodeAsync(string couponCode)
    {
        var couponFromDb = await _db.Coupons
            .FirstOrDefaultAsync(value => value.CouponCode == couponCode);

        return _mapper.Map<CouponDTO>(couponFromDb);
    }
}