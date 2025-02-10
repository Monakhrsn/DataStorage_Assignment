using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<bool> AlreadyExistAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> CreateAsync(TEntity? entity);
    Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> UpdateOneAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
}