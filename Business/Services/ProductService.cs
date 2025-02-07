using Business.Dtos;
using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
      var productList = await productRepository.GetAllAsync();
      return productList.Select(p => new Product(p.Id, p.ProductName, p.Price));
    }
}