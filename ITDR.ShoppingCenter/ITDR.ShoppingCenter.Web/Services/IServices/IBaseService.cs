using ITDR.ShoppingCenter.Web.Models;

namespace ITDR.ShoppingCenter.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDTO responseModel { get; set; }
    Task<T> SendAsync<T>(APIRequest apiRequest);
}