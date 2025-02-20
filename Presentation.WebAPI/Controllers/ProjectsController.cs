using Business.Dtos;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebAPI.Exceptions;

namespace Presentation.WebAPI.Controllers;

[Route("/api/projects")]
[ApiController]

public class ProjectsController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var result = await _projectService.CreateProjectAsync(form);
            return result ? Ok(result) : Problem("Something went wrong for creating project");
        }
        catch (ProjectAlreadyExistedException e)
        {
            // return Problem($"Project with name {form.Title} already exists", statusCode: 409);
            return Conflict();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProjects()   
    {
        var result = await _projectService.GetProjectsAsync();
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var result = await _projectService.GetProjectByIdAsync(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] ProjectUpdateForm form, int id)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        
        var result = await _projectService.UpdateProjectAsync(form, id);
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProjectById(int id)
    {
        var result = await _projectService.DeleteProjectByIdAsync(id);
        return result ? Ok(result) : NotFound();
    }

    
    
}