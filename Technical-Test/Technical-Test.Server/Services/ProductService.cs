using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<CategoryResponse>> GetProductsAsync(GetProductRequest request)
    {
        return await _context.Product.Where(o => request.Category == 0 || o.CategoryId == request.Category)
            .Include(p => p.Category)
            .Select(p => new CategoryResponse
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

    public async Task<ProductCreatedResponse> CreateProductAsync(CreateProductRequest request)
    {

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

        return new ProductCreatedResponse
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category?.CategoryName,
            CreatedAt = DateTime.UtcNow
        };
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

}
