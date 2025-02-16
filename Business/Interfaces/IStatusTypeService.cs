using Data.Dtos;
using Data.Entities;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<IEnumerable<StatusType>> GetStatusTypesAsync();
}