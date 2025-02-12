using Business.Dtos;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[Route("/api/users")]
[ApiController]

public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await _userService.CreateUserAsync(form);
        return result ? Ok() : Problem();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()   
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UserUpdateForm form, int id)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        
        var result = await _userService.UpdateUserAsync(form, id);
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCById(int id)
    {
        var result = await _userService.DeleteUserByIdAsync(id);
        return result ? Ok(result) : NotFound();
    }
    
}
