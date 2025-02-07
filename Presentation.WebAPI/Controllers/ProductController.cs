using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers;

[Route("api/products")]
[ApiController]

public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }
}