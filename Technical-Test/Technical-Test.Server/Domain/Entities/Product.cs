using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    // FK column: products.category
    public int? CategoryId { get; set; }

    // Navigation
    public Category? Category { get; set; }
}
