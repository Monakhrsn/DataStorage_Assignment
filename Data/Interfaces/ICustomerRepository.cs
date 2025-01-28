using System.Linq.Expressions;
using Data.Entities;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(CustomerEntity entity);
    Task<bool> DeleteOneAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<IEnumerable<CustomerEntity>> GetAllAsync();
    Task<CustomerEntity?> GetOneAsync(Expression<Func<CustomerEntity, bool>> expression);
    Task<bool> UpdateOneAsync(CustomerEntity updatedEntity);
}