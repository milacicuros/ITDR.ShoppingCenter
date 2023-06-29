namespace ITDR.ShoppingCenter.Services.ShoppingCartAPI.DTOs;

public class CartDTO
{
    public CartHeaderDTO CartHeader { get; set; }
    public IEnumerable<CartDetailsDTO> CartDetails { get; set; }
}