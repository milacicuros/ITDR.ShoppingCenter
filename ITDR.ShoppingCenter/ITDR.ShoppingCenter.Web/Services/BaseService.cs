using System.Net.Http.Headers;
using System.Text;
using ITDR.ShoppingCenter.Web.Models;
using ITDR.ShoppingCenter.Web.Services.IServices;
using Newtonsoft.Json;

namespace ITDR.ShoppingCenter.Web.Services;

public class BaseService : IBaseService
{
    public ResponseDTO responseModel { get; set; }
    public IHttpClientFactory httpClient { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        this.responseModel = new ResponseDTO();
        this.httpClient = httpClient;
    }
    
    public async Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        try
        {
            var client = httpClient.CreateClient("ShoppingCenterAPI");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept","application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            client.DefaultRequestHeaders.Clear();
            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8,
                    "application/json");
            }

            if (!string.IsNullOrEmpty(apiRequest.AccessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
            }

            HttpResponseMessage apiResponse = null;
            switch (apiRequest.APIType)
            {
                case SD.APIType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case SD.APIType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case SD.APIType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default :
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);

            return apiResponseDTO;
        }
        catch (Exception e)
        {
            var DTO = new ResponseDTO
            {
                DisplayMessage = "Error",
                ErrorMessage = new List<string> { Convert.ToString(e.Message) },
                IsSuccess = false
            };
            var res = JsonConvert.SerializeObject(DTO);
            var apiResponseDTO = JsonConvert.DeserializeObject<T>(res);

            return apiResponseDTO;
        }
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}