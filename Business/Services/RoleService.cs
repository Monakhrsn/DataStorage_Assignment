using Business.Dtos;
using Business.Interfaces;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        var rolls = await roleRepository.GetAllAsync();
        return rolls.Select(r => new Role(r.Id, r.RoleName ));
    }
}
