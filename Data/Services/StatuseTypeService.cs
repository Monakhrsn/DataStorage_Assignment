using Data.Dtos;
using Data.Interfaces;

namespace Data.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    public async Task<IEnumerable<StatusType>> GetStatusTypesAsync()
    {
        var statusTypeList = await statusTypeRepository.GetAllAsync();
        return statusTypeList.Select(st => new StatusType(st.Id, st.StatusName));
    }
}