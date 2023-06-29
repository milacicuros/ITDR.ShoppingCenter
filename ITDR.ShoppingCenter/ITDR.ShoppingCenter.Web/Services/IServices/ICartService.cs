﻿using ITDR.ShoppingCenter.Web.Models;

namespace ITDR.ShoppingCenter.Web.Services.IServices;

public interface ICartService
{
    Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
    Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null);
    Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null);
    Task<T> RemoveFromCartAsync<T>(int cartId, string token = null); //cartDetailsId
}