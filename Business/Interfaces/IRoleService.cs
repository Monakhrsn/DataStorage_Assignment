using Business.Dtos;

namespace Business.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetRolesAsync();
}