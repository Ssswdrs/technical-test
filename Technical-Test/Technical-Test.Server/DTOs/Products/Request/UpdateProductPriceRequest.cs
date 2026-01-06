
namespace Technical_Test.Server.DTOs.Products.Request;
public class UpdateProductPriceRequest
{
    public int ProductId { get; set; }
    public decimal NewPrice { get; set; }
}
