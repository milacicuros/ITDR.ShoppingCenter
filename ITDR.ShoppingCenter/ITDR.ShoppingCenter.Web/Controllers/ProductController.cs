﻿using ITDR.ShoppingCenter.Web.Models;
using ITDR.ShoppingCenter.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ITDR.ShoppingCenter.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IActionResult> ProductIndex()
    {
        var list = new List<ProductDTO>();
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _productService.GetAllProductsAsync<ResponseDTO>(accessToken);
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
        }
        return View(list);
    }
    
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCreate(ProductDTO model)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");  
            var response = await _productService.CreateProductAsync<ResponseDTO>(model, accessToken);
            if (response != null && response.IsSuccess)
            { 
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        
        return View(model);
    }
    
    public async Task<IActionResult> ProductEdit(int productId)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _productService.GetProductByIdAsync<ResponseDTO>(productId, accessToken);
        if (response != null && response.IsSuccess)
        { 
            var model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            return View(model);
        }
        return NotFound();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDTO model)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.UpdateProductAsync<ResponseDTO>(model, accessToken);
            if (response != null && response.IsSuccess)
            { 
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        
        return View(model);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ProductDelete(int productId)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _productService.GetProductByIdAsync<ResponseDTO>(productId, accessToken);
        if (response != null && response.IsSuccess)
        { 
            ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            return View(model);
        }
        return NotFound();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductDelete(ProductDTO model)
    {
        if (!ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductAsync<ResponseDTO>(model.ProductId, accessToken);
            if (response.IsSuccess)
            { 
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        
        return View(model);
    }
}