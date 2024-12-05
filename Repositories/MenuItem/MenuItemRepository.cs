using Cafe_Management_System.Data;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.MenuItemDto;
using Cafe_Management_System.Services.CloudinaryService;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Repositories.MenuItem;

public class MenuItemRepository(
    AppDbContext context,
    CloudinaryService cloudinaryService
)
    : IMenuItemReposiroty
{
    private readonly AppDbContext _context = context;
    private readonly CloudinaryService _cloudinaryService = cloudinaryService;

    public async Task AddMenuItem(AddMenuItemDto item, IFormFile file)
    {
        var category = await _context.Categories.FindAsync(item.CategoryId) ??
                       throw new KeyNotFoundException("Category Not Found");
        var imageUrl = await _cloudinaryService.UploadImage(file);
        var addedMenuItem = item.ToMenuItem(category, imageUrl);
        await _context.MenuItems.AddAsync(addedMenuItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMenuItem(string id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id) ?? throw new KeyNotFoundException("MenuItem Not Found");
        await _context.MenuItems.Where(e => e.MenuItemId == id).ExecuteDeleteAsync();
    }

    public async Task<ReadMenuItemDto> GetMenuItemById(string id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id) ?? throw new KeyNotFoundException("MenuItem Not Found");
        var category = await _context.Categories.FindAsync(menuItem.CategoryId) ??
                       throw new KeyNotFoundException("Category Not Found");
        return menuItem.ToReadMenuItemDto(category.ToReadCategoryDto());
    }

    public async Task<List<ReadMenuItemDto>> GetAllMenuItems()
    {
        var menuItem = await _context.MenuItems.ToListAsync() ?? throw new KeyNotFoundException("MenuItem Not Found");
        var menuItems = await Task.WhenAll(menuItem.Select(async e =>
        {
            var category = await _context.Categories.FindAsync(e.CategoryId) ??
                           throw new KeyNotFoundException("Category Not Found");
            return e.ToReadMenuItemDto(category.ToReadCategoryDto());
        }));
        return menuItems.ToList();
    }

    public async Task<List<ReadMenuItemDto>> GetAllMenuItemsByCategory(string categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId) ??
                       throw new KeyNotFoundException("Category Not Found");
        var menuItem = await _context.MenuItems.Where(e => e.CategoryId == categoryId).ToListAsync() ??
                       throw new KeyNotFoundException("Category Not Found");
        return menuItem.Select(e =>
            e.ToReadMenuItemDto(category.ToReadCategoryDto())).ToList();
    }

    public async Task UpdateMenuItems(AddMenuItemDto addMenuItemDto, string menuItemId)
    {
        var oldMenuItem = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(e => e.MenuItemId == menuItemId)
            ?? throw new KeyNotFoundException("MenuItem Not Found");
        if (oldMenuItem.CategoryId != addMenuItemDto.CategoryId)
        {
            oldMenuItem.ToUpdatedMenuItems(addMenuItemDto, oldMenuItem.Category);
        }
        else
        {
            var category = await _context.Categories.FindAsync(addMenuItemDto.CategoryId) ?? throw new KeyNotFoundException("Category Not Found");
            oldMenuItem.ToUpdatedMenuItems(addMenuItemDto, category);
        }

        _context.MenuItems.Update(oldMenuItem);
        await _context.SaveChangesAsync();
        
    }
}