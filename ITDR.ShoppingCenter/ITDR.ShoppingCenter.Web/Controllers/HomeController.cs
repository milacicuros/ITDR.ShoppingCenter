using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ITDR.ShoppingCenter.Web.Models;
using ITDR.ShoppingCenter.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace ITDR.ShoppingCenter.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(
        ILogger<HomeController> logger, 
        IProductService productService,
        ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var list = new List<ProductDTO>();
        var response = await _productService.GetAllProductsAsync<ResponseDTO>("");
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
        }
        
        return View(list);
    }
    
    [Authorize]
    public async Task<IActionResult> Details(int productId)
    {
        var model = new ProductDTO();
        var response = await _productService.GetProductByIdAsync<ResponseDTO>(productId, "");
        if (response != null && response.IsSuccess)
        {
            model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
        }
        
        return View(model);
    }
    
    [Authorize]
    [HttpPost]
    [ActionName("Details")]
    public async Task<IActionResult> DetailsPost(ProductDTO productDTO)
    {
            var cartDTO = new CartDTO
            {
                CartHeader = new CartHeaderDTO
                {
                    UserId = User.Claims
                        .Where(claim => claim.Type == "sub")?
                        .FirstOrDefault()?.Value,
                    //CouponCode = "00x0"//, da radi AddCart
                    
                }
            };

            var cartDetailsDTO = new CartDetailsDTO
            {
                Count = productDTO.Count,
                ProductId = productDTO.ProductId,
                //CartHeaderId = cartDTO.CartHeader.CartHeaderId,// da radi AddCart
                //CartHeader = cartDTO.CartHeader//, da radi AddCart
            };

            var response = await _productService.GetProductByIdAsync<ResponseDTO>(productDTO.ProductId, "");
            if (response != null && response.IsSuccess)
            {
                cartDetailsDTO.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }

            var cartDetailsList = new List<CartDetailsDTO>();
            cartDetailsList.Add(cartDetailsDTO);
            cartDTO.CartDetails = cartDetailsList;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResponse = await _cartService.AddToCartAsync<ResponseDTO>(cartDTO, accessToken);
            if (addToCartResponse != null && addToCartResponse.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public async Task<IActionResult> Login()
    {
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}