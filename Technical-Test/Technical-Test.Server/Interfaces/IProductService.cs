using Technical_Test.Server.DTOs.Products.Request;
using Technical_Test.Server.DTOs.Products.Response;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<CategoryResponse>> GetProductsAsync(GetProductRequest request);
    Task<ProductCreatedResponse> CreateProductAsync(CreateProductRequest request);

    Task<StatusResponse> SellProductAsync(SellProductRequest request);
}
