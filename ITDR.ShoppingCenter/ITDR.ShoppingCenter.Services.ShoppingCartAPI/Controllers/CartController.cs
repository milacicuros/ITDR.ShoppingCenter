using ITDR.ShoppingCenter.Services.ShoppingCartAPI.DTOs;
using ITDR.ShoppingCenter.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITDR.ShoppingCenter.Services.ShoppingCartAPI.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : Controller
{
    private readonly ICartRepository _cartRepository;
    protected ResponseDTO _response;
    
    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
        this._response = new ResponseDTO();
    }
    
    [HttpGet("GetCart/{userId}")]
    public async Task<object> GetCart(string userId)
    {
        try
        {
            var cartDTO = await _cartRepository.GetCartByUserId(userId);
            _response.Result = cartDTO;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    [HttpPost("AddCart")]
    public async Task<object> AddCart(CartDTO cartDTO)
    {
        try
        {
            var cart = await _cartRepository.CreateUpdateCart(cartDTO);
            _response.Result = cart;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    [HttpPost("UpdateCart")]
    public async Task<object> UpdateCart(CartDTO cartDTO)
    {
        try
        {
            var cart = await _cartRepository.CreateUpdateCart(cartDTO);
            _response.Result = cart;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    [HttpPost("RemoveCart")]
    public async Task<object> RemoveCart([FromBody] int cartId)
    {
        try
        {
            bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    [HttpPost("ApplyCoupon")]
    public async Task<object> ApplyCoupon([FromBody] CartDTO cartDTO)
    {
        try
        {
            bool isSuccess = await _cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId, 
                cartDTO.CartHeader.CouponCode);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    [HttpPost("RemoveCoupon")]
    public async Task<object> RemoveCoupon([FromBody] string userId)
    {
        try
        {
            bool isSuccess = await _cartRepository.RemoveCoupon(userId);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>() { e.ToString() };
        }

        return _response;
    }
}