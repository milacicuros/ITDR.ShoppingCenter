using ITDR.ShoppingCenter.Services.ProductAPI.DTOs;
using ITDR.ShoppingCenter.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITDR.ShoppingCenter.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductAPIController : ControllerBase
{
    protected ResponseDTO _response;

    private IProductRepository _productRepository;

    public ProductAPIController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        this._response = new ResponseDTO();
    }
    
    [HttpGet]
    public async Task<object> Get()
    {
        try
        {
            var productsDTO = await _productRepository.GetProducts();
            _response.Result = productsDTO;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage
                = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<object> Get(int id)
    {
        try
        {
            var productDTO = await _productRepository.GetProductById(id);
            _response.Result = productDTO;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage
                = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<object> Post([FromBody] ProductDTO productDTO)
    {
        try
        {
            var model = await _productRepository.CreateUpdateProduct(productDTO);
            _response.Result = model;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage
                = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [Authorize]
    [HttpPut]
    public async Task<object> Put([FromBody] ProductDTO productDTO)
    {
        try
        {
            var model = await _productRepository.CreateUpdateProduct(productDTO);
            _response.Result = model;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage
                = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<object> Delete(int id)
    {
        try
        {
            bool isSuccess = await _productRepository.DeleteProduct(id);
            _response.Result = isSuccess;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage
                = new List<string>() { ex.ToString() };
        }

        return _response;
    }
}