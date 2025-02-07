using Data.Interfaces;
using Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[Route("/api/statusTypes")]
[ApiController]

public class StatusTypeController(IStatusTypeService statusTypeService) : ControllerBase
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    
    [HttpGet]
    public async Task<IActionResult> GetStatusTypes()
    {
        var statusTypes = await _statusTypeService.GetStatusTypesAsync();
        return Ok(statusTypes);
    }
}