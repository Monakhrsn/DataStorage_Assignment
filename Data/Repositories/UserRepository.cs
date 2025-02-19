using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
    private readonly DataContext _context = context;
    
    // Override, fetch users with their roles
    public override async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }
    
    // Override GetOneAsync to includ role
    public override async Task<UserEntity?> GetOneAsync(Expression<Func<UserEntity, bool>> expression)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(expression);
    }
    
    public async Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(int id)
    {
        return await _context.Users.Include(u => u.Role)
            .Include(u => u.Role) 
            .Where(u => u.Role.Id == id)
            .ToListAsync();
    }
}