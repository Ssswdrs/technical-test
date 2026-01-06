using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Technical_Test.Server.DTOs.Products.Request;
using Technical_Test.Server.DTOs.Products.Response;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync(GetProductRequest request)
    {
        return await _context.Product.Where(o => request.Category == 0 || o.CategoryId == request.Category)
            .Include(p => p.Category)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                Category = p.Category != null
                    ? p.Category.CategoryName
                    : null
            })
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsByKeywordAsync(GetProductRequest request)
    {
        var result = await _context.Product
                                .Where(o => string.IsNullOrEmpty(request.Keyword) || (o.Name.ToLower().Contains(request.Keyword.ToLower())
                                         || o.Sku.ToLower().Contains(request.Keyword.ToLower())) && (request.Category ==  0 || o.CategoryId == request.Category))
                                .Include(p => p.Category)
                                .Select(p => new ProductResponse
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    Sku = p.Sku,
                                    Price = p.Price,
                                    Stock = p.Stock,
                                    CategoryId = p.CategoryId,
                                    Category = p.Category != null ? p.Category.CategoryName : null
                                })
                                .OrderBy(p => p.Name)
                                .ToListAsync();
        return result;
    }

    public async Task<(bool Success, ProductCreatedResponse? Product, List<string>? Errors)> CreateProductAsync(CreateProductRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.Name))
            errors.Add("ชื่อสินค้าต้องไม่ว่าง");

        if (string.IsNullOrWhiteSpace(request.Sku) || request.Sku.Length < 3)
            errors.Add("รหัสสินค้าต้องมีอย่างน้อย 3 ตัวอักษร");

        if (request.Price <= 0)
            errors.Add("ราคาต้องมากกว่า 0");

        if (_context.Product.Any(o => o.Sku == request.Sku))
            errors.Add("SKU ซ้ำกับสินค้าที่มีอยู่แล้ว");

        if (errors.Any())
        {
            return (false, null, errors); // ยังไม่ insert, return errors
        }

        // Create product
        var product = new Product
        {
            Name = request.Name,
            Sku = request.Sku,
            Price = request.Price,
            Stock = request.Stock,
            CategoryId = request.CategoryId
        };

        _context.Product.Add(product);
        await _context.SaveChangesAsync();

        await _context.Entry(product)
            .Reference(p => p.Category)
            .LoadAsync();

        var response = new ProductCreatedResponse
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category?.CategoryName,
            CreatedAt = DateTime.UtcNow
        };

        return (true, response, null);
    }



    public async Task<StatusResponse> SellProductAsync(SellProductRequest request)
    {
        try
        {
            Product? product = await _context.Product
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                return new StatusResponse
                {
                    Status = "NotFound"
                };
            }
            if(request.Quantity <= 0)
            {
                return new StatusResponse
                {
                    Status = "QuantityBelowZero"
                };
            }

            if((product.Stock - request.Quantity) >= 0)
            {
                product.Stock -= request.Quantity;
            }
            else
            {
                return new StatusResponse
                {
                    Status = "OutOfStock"
                };
            }
            await _context.SaveChangesAsync();
            return new StatusResponse
            {
                Status = "Success"
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<UpdatePriceResponse> BulkPriceUpdate(IEnumerable<UpdateProductPriceRequest> requests)
    {
        var requestList = requests.ToList();
        var productIds = requestList.Select(r => r.ProductId).ToList();

        var products = await _context.Product
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        var notFoundIds = productIds.Except(products.Select(p => p.Id)).ToList();

        foreach (var product in products)
        {
            var req = requestList.First(r => r.ProductId == product.Id);
            product.Price = req.NewPrice;
        }

        if (products.Any())
        {
            _context.UpdateRange(products);
            await _context.SaveChangesAsync();
        }

        return new UpdatePriceResponse
        {
            TotalRequested = requestList.Count,
            TotalUpdated = products.Count,
            NotFoundProductIds = notFoundIds
        };
    }


}
