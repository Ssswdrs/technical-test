namespace Technical_Test.Server.DTOs.Products.Request
{
    public class GetProductRequest
    {
        public string? Keyword { get; set; } = string.Empty;
        public int? Category { get; set; }
    }
}
