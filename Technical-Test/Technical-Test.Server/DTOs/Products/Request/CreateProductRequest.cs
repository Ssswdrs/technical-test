
namespace Technical_Test.Server.DTOs.Products.Request;
public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
}
