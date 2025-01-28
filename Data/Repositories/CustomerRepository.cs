using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context)
{
    private readonly DataContext _context = context;
    
    // CREATE
    public async Task<bool> AddAsync(CustomerEntity entity)
    {
        try
        {
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
           return false;
        }
    }
    
    // READ
    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<CustomerEntity?> GetAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(expression);
        return entity;
    }
    
    // UPDATE
    public async Task<bool> UpdateAsync(CustomerEntity updatedEntity)
    {
        try
        {
            var entity = await _context.Customers.FindAsync(updatedEntity);
            if (entity == null) return false;
            _context.Entry(entity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return false;
        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}