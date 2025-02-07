using Data.Entities;

namespace Data.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductEntity>> GetAllAsync();
}