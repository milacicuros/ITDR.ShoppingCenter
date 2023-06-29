using AutoMapper;
using ITDR.ShoppingCenter.Services.ProductAPI.DbContexts;
using ITDR.ShoppingCenter.Services.ProductAPI.DTOs;
using ITDR.ShoppingCenter.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ITDR.ShoppingCenter.Services.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    private IMapper _mapper;

    public ProductRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsList = await _db.Products.ToListAsync();
        return _mapper.Map<List<ProductDTO>>(productsList);
    }

    public async Task<ProductDTO> GetProductById(int productId)
    {
        var products = await _db.Products
            .Where(product => product.ProductId == productId)
            .FirstOrDefaultAsync();
        return _mapper.Map<ProductDTO>(products);
    }

    public async Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO)
    {
        var products = _mapper.Map<ProductDTO, Product>(productDTO);
        if (products.ProductId > 0)
        {
            _db.Products.Update(products);
        }
        else
        {
            _db.Products.Add(products);
        }

        await _db.SaveChangesAsync();
        return _mapper.Map<Product,ProductDTO>(products);
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            var products = await _db.Products.FirstOrDefaultAsync(product => product.ProductId == productId);
            if (products == null)
            {
                return false;
            }
            _db.Products.Remove(products);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}