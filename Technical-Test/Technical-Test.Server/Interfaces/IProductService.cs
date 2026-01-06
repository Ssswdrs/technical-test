using Technical_Test.Server.DTOs.Products.Request;
using Technical_Test.Server.DTOs.Products.Response;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetProductsAsync(GetProductRequest request);
    Task<IEnumerable<ProductResponse>> GetProductsByKeywordAsync(GetProductRequest request);
    Task<(bool Success, ProductCreatedResponse? Product, List<string>? Errors)> CreateProductAsync(CreateProductRequest request);

    Task<StatusResponse> SellProductAsync(SellProductRequest request);
    Task<UpdatePriceResponse> BulkPriceUpdate(IEnumerable<UpdateProductPriceRequest> request);
}
