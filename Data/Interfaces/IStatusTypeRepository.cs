using Data.Entities;

namespace Data.Interfaces;

public interface IStatusTypeRepository
{
    Task<IEnumerable<StatusTypeEntity>> GetAllStatusTypesAsync();
    Task<StatusTypeEntity> GetStatusTypeByIdAsync(int statusTypeId);
    Task<StatusTypeEntity> UpdateOneStatusTypeByIdAsync(int statusTypeId, string statusName);
}