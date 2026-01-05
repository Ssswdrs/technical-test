using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Technical_Test.Server.DTOs.Products.Request;
using Technical_Test.Server.DTOs.Products.Response;

[ApiController]
[Route("/api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<CategoryResponse>> GetCategoryAsync()
    {
        var result = await _service.GetCategoryAsync();

        return Ok(result);
    }
}
