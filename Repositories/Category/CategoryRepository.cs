using Cafe_Management_System.Data;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Repositories.Category;

public class CategoryRepository(
    AppDbContext context
    )
    :ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddCategory(AddCategoryDto category)
    {
        var newCategory = category.ToCategory();
        await _context.AddAsync(newCategory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategory(string id)
    {
        var existingCategory = await _context.Categories.FindAsync(id)?? throw new KeyNotFoundException("Category Not Found");
        await _context.Categories.Where(c => c.CategoryId == id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategory(AddCategoryDto category, string id)
    {
        var existingCategory = await _context.Categories.FindAsync(id)?? throw new KeyNotFoundException("Category Not Found");
        existingCategory.UpdateCategories(category);
        _context.Categories.Update(existingCategory);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ReadCategoryDto>> GetAllCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories.Select(e => e.ToReadCategoryDto()).ToList();
    }

    public async Task<ReadCategoryDto> GetCategoryById(string id)
    {
        var category = await _context.Categories.FindAsync(id)?? throw new KeyNotFoundException("Category Not Found");
        return category.ToReadCategoryDto();
        
    }
    
}