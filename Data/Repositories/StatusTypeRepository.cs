using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context) : IStatusTypeRepository
{
    private readonly DataContext _context = context;

    //READ ALL
    public async Task<IEnumerable<StatusTypeEntity>> GetAllAsync()
    {
        return await _context.StatusTypes.ToListAsync();
    }
}