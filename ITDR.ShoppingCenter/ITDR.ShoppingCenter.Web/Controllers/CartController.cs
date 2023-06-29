using ITDR.ShoppingCenter.Web.Models;
using ITDR.ShoppingCenter.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ITDR.ShoppingCenter.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(IProductService productService, 
        ICartService cartService, ICouponService couponService)
    {
        _productService = productService;
        _cartService = cartService;
        _couponService = couponService;
    }
    
    public async Task<IActionResult> CartIndex()
    {
        return View(await LoadCartDTOBasedOnLoggedInUser());
    }
    
    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
    {
        var userId = User.Claims
            .Where(claim => claim.Type == "sub")?
            .FirstOrDefault()?.Value;
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _cartService.ApplyCouponAsync<ResponseDTO>(cartDTO, accessToken);
        
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return Problem(); //treba View()
    }
    
    [HttpPost]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
    {
        var userId = User.Claims
            .Where(claim => claim.Type == "sub")?
            .FirstOrDefault()?.Value;
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _cartService.RemoveCouponAsync<ResponseDTO>(cartDTO.CartHeader.UserId, accessToken);
        
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return Problem(); //treba View()
    }
    
    public async Task<IActionResult> Remove(int cartDetailsId)
    {
        var userId = User.Claims
            .Where(claim => claim.Type == "sub")?
            .FirstOrDefault()?.Value;
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _cartService.RemoveFromCartAsync<ResponseDTO>(cartDetailsId, accessToken);
        
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return Problem(); // treba View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        return View(await LoadCartDTOBasedOnLoggedInUser());
    }

    private async Task<CartDTO> LoadCartDTOBasedOnLoggedInUser()
    {
        var userId = User.Claims
            .Where(claim => claim.Type == "sub")?
            .FirstOrDefault()?.Value;
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _cartService.GetCartByUserIdAsync<ResponseDTO>(userId, accessToken);

        var cartDTO = new CartDTO();
        if (response != null && response.IsSuccess)
        {
            cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
        }

        if (cartDTO.CartHeader != null)
        {
            if (!string.IsNullOrEmpty(cartDTO.CartHeader.CouponCode))
            {
                var coupon = await _couponService
                    .GetCouponAsync<ResponseDTO>(cartDTO.CartHeader.CouponCode, accessToken);
                if (coupon != null && coupon.IsSuccess)
                {
                    var couponDTO = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(coupon.Result));
                    cartDTO.CartHeader.DiscountTotal = couponDTO.DiscountAmount;
                }
            }
            
            foreach (var detail in cartDTO.CartDetails)
            {
                cartDTO.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
            }

            cartDTO.CartHeader.OrderTotal -= cartDTO.CartHeader.DiscountTotal;
        }

        return cartDTO;
    }
}