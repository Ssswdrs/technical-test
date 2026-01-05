using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Technical_Test.Server.DTOs.Products.Request;
using Technical_Test.Server.DTOs.Products.Response;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ProductCreatedResponse>> GetProductsAsync([FromQuery] GetProductRequest request)
    {
        var result = await _service.GetProductsAsync(request);

        return Ok(result);
    }

    [HttpDelete("sell")]
    public async Task<ActionResult<StatusResponse>> SellProductAsync([FromBody] SellProductRequest request)
    {
        var result = await _service.SellProductAsync(request);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductCreatedResponse>> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        var result = await _service.CreateProductAsync(request);

        return Ok(result);
    }
}
