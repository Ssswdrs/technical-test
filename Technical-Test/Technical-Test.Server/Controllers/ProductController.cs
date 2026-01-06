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
    public async Task<ActionResult<ProductResponse>> GetProductsAsync([FromQuery] GetProductRequest request)
    {
        var result = await _service.GetProductsAsync(request);

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<ProductResponse>> GetProductsByKeywordAsync([FromQuery] GetProductRequest request)
    {
        var result = await _service.GetProductsByKeywordAsync(request);

        return Ok(result);
    }

    [HttpPost("sell")]
    public async Task<ActionResult<StatusResponse>> SellProductAsync([FromBody] SellProductRequest request)
    {
        var result = await _service.SellProductAsync(request);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var result = await _service.CreateProductAsync(request);

        if (!result.Success)
        {
            // Return 400 
            return BadRequest(new { errors = result.Errors });
        }

        // Return 200 
        return Ok(result.Product);
    }

    [HttpPut("bulk-price-update")]
    public async Task<IActionResult> BulkPriceUpdate([FromBody] IEnumerable<UpdateProductPriceRequest> request)
    {
        var result = await _service.BulkPriceUpdate(request);

        return Ok(result);
    }

}
