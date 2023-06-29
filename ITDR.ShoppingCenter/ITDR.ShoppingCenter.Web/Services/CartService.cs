using ITDR.ShoppingCenter.Web.Models;
using ITDR.ShoppingCenter.Web.Services.IServices;

namespace ITDR.ShoppingCenter.Web.Services;

public class CartService : BaseService, ICartService
{
    private readonly IHttpClientFactory _clientFactory;
    
    public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId,
            AccessToken = token
        });
    }

    public async Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
            AccessToken = token
        });
    }

    public async Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
            AccessToken = token
        });
    }

    public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            Data = cartId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
            AccessToken = token
        });
    }

    public async Task<T> ApplyCouponAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
            AccessToken = token
        });
    }

    public async Task<T> RemoveCouponAsync<T>(string userId, string token = null)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            Data = userId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon",
            AccessToken = token
        });
    }
}