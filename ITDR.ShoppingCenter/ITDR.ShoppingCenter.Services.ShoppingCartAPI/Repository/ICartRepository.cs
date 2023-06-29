using ITDR.ShoppingCenter.Services.ShoppingCartAPI.DTOs;

namespace ITDR.ShoppingCenter.Services.ShoppingCartAPI.Repository;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserId(string userId);
    Task<CartDTO> CreateUpdateCart(CartDTO cartDTO);
    Task<bool> RemoveFromCart(int cartDetailsId);
    Task<bool> ClearCart(string userId);
}