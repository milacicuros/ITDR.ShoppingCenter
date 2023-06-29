using ITDR.ShoppingCenter.Web.Models;
using ITDR.ShoppingCenter.Web.Services.IServices;

namespace ITDR.ShoppingCenter.Web.Services;

public class CouponService : BaseService, ICouponService
{
    private readonly IHttpClientFactory _clientFactory;
    
    public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    public async Task<T> GetCouponAsync<T>(string couponCode, string token = null)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
            AccessToken = token
        });
    }
}