using System.Diagnostics;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[Route("/api/rolles")]
[ApiController]

public class RollesController(IRoleService roleService): ControllerBase
{
    private readonly IRoleService _roleService = roleService;
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetRolles()
    {
        try
        {
            var roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetProducts: { ex.Message }");
        }
        return StatusCode(500, new { error = "Ann error occured while fetching rolles." });
    }
}