namespace ITDR.ShoppingCenter.Web.Models;

public class APIRequest
{
    public SD.APIType APIType { get; set; } = SD.APIType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
}