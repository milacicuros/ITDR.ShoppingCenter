﻿using ITDR.ShoppingCenter.Services.ShoppingCartAPI.DTOs;

namespace ITDR.ShoppingCenter.Services.ShoppingCartAPI.Repository;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserId(string userId);
    Task<CartDTO> CreateUpdateCart(CartDTO cartDTO);
    Task<bool> RemoveFromCart(int cartDetailsId);
    Task<bool> ApplyCoupon(string userId, string couponCode);
    Task<bool> RemoveCoupon(string userId);
    Task<bool> ClearCart(string userId);
}