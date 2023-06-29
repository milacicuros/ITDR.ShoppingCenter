using ITDR.ShoppingCenter.Services.CouponAPI.DTOs;
using ITDR.ShoppingCenter.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITDR.ShoppingCenter.Services.CouponAPI.Controllers;

[ApiController]
[Route("api/coupon")]
public class CouponController : Controller
{
    private readonly ICouponRepository _couponRepository;
    protected ResponseDTO _response;
    
    public CouponController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
        this._response = new ResponseDTO();
    }
    
    [HttpGet("{code}")]
    public async Task<object> GetDiscountForCode(string code)
    {
        try
        {
            var couponDTO = await _couponRepository.GetCouponByCodeAsync(code);
            _response.Result = couponDTO;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() { e.ToString() };
        }

        return _response;
    }
}