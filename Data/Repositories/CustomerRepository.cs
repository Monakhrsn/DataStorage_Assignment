using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : ICustomerRepository
{
    private readonly DataContext _context = context;
    
    // CREATE
    public async Task<bool> CreateAsync(CustomerEntity entity)
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

    public async Task<CustomerEntity?> GetOneAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(expression);
        return entity;
    }
    
    // UPDATE
    public async Task<bool> UpdateOneAsync(CustomerEntity updatedEntity)
    {
        try
        {
            var entity = await _context.Customers.FindAsync(updatedEntity);
            if (entity == null) 
                return false;
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
    public async Task<bool> DeleteOneAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(expression);
        if (entity == null) 
            return false;
        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}