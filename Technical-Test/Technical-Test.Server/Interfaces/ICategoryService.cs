using Technical_Test.Server.DTOs.Products.Request;
using Technical_Test.Server.DTOs.Categories.Response;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetCategoryAsync();
}
