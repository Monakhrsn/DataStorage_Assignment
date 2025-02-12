using Business.Dtos;
using Data.Dtos;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[Route("/api/customers")]
[ApiController]

public class CustomersController(ICustomerService customerService) : ControllerBase
{ 
    private readonly ICustomerService _customerService = customerService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _customerService.CreateCustomerAsync(form);
        return result ? Ok() : Problem();
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()   
    {
        var result = await _customerService.GetCustomersAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var result = await _customerService.GetCustomerByIdAsync(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
         
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] CustomerUpdateForm form, int id)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        
        var result = await _customerService.UpdateCustomerAsync(form, id);
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCustomerById(int id)
    {
        var result = await _customerService.DeleteCustomerByIdAsync(id);
        return result ? Ok(result) : NotFound();
    }
}