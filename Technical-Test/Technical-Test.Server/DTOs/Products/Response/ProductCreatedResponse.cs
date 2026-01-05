namespace Technical_Test.Server.DTOs.Products.Response;

public class ProductCreatedResponse
{
    public int? Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    // ส่งชื่อหมวดหมู่ ไม่ expose Id
    public string? Category { get; set; }

    // ใช้ UTC ตาม ISO 8601
    public DateTime CreatedAt { get; set; }
}
