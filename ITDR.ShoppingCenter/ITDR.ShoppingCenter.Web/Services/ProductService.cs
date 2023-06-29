using ITDR.ShoppingCenter.Web.Models;
using ITDR.ShoppingCenter.Web.Services.IServices;

namespace ITDR.ShoppingCenter.Web.Services;

public class ProductService : BaseService, IProductService
{
    private IHttpClientFactory _clientFactory;
    
    public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<T> GetAllProductsAsync<T>(string token)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            Url = SD.ProductAPIBase + "/api/products",
            AccessToken = token
        });
    }

    public async Task<T> GetProductByIdAsync<T>(int id, string token)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            Url = SD.ProductAPIBase + "/api/products/" + id,
            AccessToken = token
        });
    }

    public async Task<T> CreateProductAsync<T>(ProductDTO productDTO, string token)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            Data = productDTO,
            Url = SD.ProductAPIBase + "/api/products",
            AccessToken = token
        });
    }

    public async Task<T> UpdateProductAsync<T>(ProductDTO productDTO, string token)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.PUT,
            Data = productDTO,
            Url = SD.ProductAPIBase + "/api/products",
            AccessToken = token
        });
    }

    public async Task<T> DeleteProductAsync<T>(int id, string token)
    {
        return await this.SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.DELETE,
            Url = SD.ProductAPIBase + "/api/products/" + id,
            AccessToken = token
        });
    }
}