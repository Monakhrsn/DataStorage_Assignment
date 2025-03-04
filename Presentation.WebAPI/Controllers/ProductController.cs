using System.Diagnostics;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[Route("/api/products")]
[ApiController]

public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetProducts: { ex.Message }");
        }
        return StatusCode(500, new { error = "Ann error occured while fetching products"});
    }
}                      