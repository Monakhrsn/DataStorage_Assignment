using Business.Dtos;
using Business.Services;

namespace Business.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync();
}