using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;
    
    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.Status)
            .Include(p => p.User)
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .ToListAsync();
    }

    public override async Task<ProjectEntity?> GetOneAsync(Expression<Func<ProjectEntity, bool>> predicate)
    {
        return await _context.Projects
            .Include(p => p.Status)
            .Include(p => p.User)
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(predicate);
    }
}
