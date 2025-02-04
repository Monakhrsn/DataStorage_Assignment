using Data.Entities;

namespace Data.Interfaces;

public interface IStatusTypeRepository
{
    Task<IEnumerable<StatusTypeEntity>> GetAllAsync();
}