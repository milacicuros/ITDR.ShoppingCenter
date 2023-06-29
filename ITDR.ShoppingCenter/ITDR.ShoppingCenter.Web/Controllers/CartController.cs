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

    public CartController(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }
    
    public async Task<IActionResult> CartIndex()
    {
        return View(await LoadCartDTOBasedOnLoggedInUser());
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

        return NoContent(); // treba View();
    }
    
    public async Task<IActionResult> Checkout()
    {
        return NoContent();
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
            foreach (var detail in cartDTO.CartDetails)
            {
                cartDTO.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
            }
        }

        return cartDTO;
    }
}