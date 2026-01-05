using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Technical_Test.Server.DTOs.Categories.Response;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    // GET
    public async Task<IEnumerable<CategoryResponse>> GetCategoryAsync()
    {
        return await _context.Category
            .Select(p => new CategoryResponse
            {
                Id = p.Id,
                CategoryName = p.CategoryName,
            })
            .ToListAsync();
    }

}
