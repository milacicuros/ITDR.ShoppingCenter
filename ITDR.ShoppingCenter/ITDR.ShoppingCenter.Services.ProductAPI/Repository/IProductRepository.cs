using ITDR.ShoppingCenter.Services.ProductAPI.DTOs;

namespace ITDR.ShoppingCenter.Services.ProductAPI.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductDTO>> GetProducts();
    Task<ProductDTO> GetProductById(int productId);
    Task<ProductDTO> CreateUpdateProduct(ProductDTO productDTO);
    Task<bool> DeleteProduct(int productId);
}