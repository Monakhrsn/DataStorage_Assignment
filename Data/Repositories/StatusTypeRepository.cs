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
    public async Task<IEnumerable<StatusTypeEntity>> GetAllStatusTypesAsync()
    {
        return await _context.StatusTypes.ToListAsync();
    }

    //READ ONE 
    public async Task<StatusTypeEntity> GetStatusTypeByIdAsync(int id)
    {
        var statusType = await _context.StatusTypes.FindAsync(id);
        if (statusType != null)
            return statusType;
        throw new Exception("Status type not found");
    }
    
    //UPDATE
    public async Task<StatusTypeEntity> UpdateOneStatusTypeByIdAsync(int statusTypeId, string updatedStatusName)
    {
        try
        {
            // Find the status type by id
            var statusType = await _context.StatusTypes.FindAsync(statusTypeId);
            if (statusType == null)
                throw new Exception("Status type not found");

            // Update the statusName
            statusType.StatusName = updatedStatusName;
            await _context.SaveChangesAsync();
            return statusType;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}