using Cafe_Management_System.Entities;
using Cafe_Management_System.Models.Category;
using Cafe_Management_System.Models.MenuItemDto;

namespace Cafe_Management_System.Mappers;

public static class MenuItemMapper
{
    public static MenuItems ToMenuItem(this AddMenuItemDto addMenuItemDto, Categories categories, string imageUrl)
    {
        return new MenuItems()
        {
            Name = addMenuItemDto.Name,
            Description = addMenuItemDto.Description,
            SellingPrice = addMenuItemDto.SellingPrice,
            CostPrice = addMenuItemDto.CostPrice,
            Category = categories,
            ImageUrl = imageUrl,
            CategoryId = addMenuItemDto.CategoryId,
            IsAvailable = addMenuItemDto.IsAvailable,
            IsVegetarian = addMenuItemDto.IsVegetarian,
            Spicy = addMenuItemDto.Spicy,
        };
    }

    public static ReadMenuItemDto ToReadMenuItemDto(this MenuItems menuItems, ReadCategoryDto category)
    {
        return new ReadMenuItemDto()
        {
            Id = menuItems.MenuItemId,
            Name = menuItems.Name,
            Category = category,
            SellingPrice = menuItems.SellingPrice,
            ImageUrl = menuItems.ImageUrl,
            CategoryId = menuItems.CategoryId,
            IsAvailable = menuItems.IsAvailable,
            IsVegetarian = menuItems.IsVegetarian,
            Description = menuItems.Description,
            Spicy = menuItems.Spicy
        };
    }

    public static void ToUpdatedMenuItems(this MenuItems menuItems, AddMenuItemDto newMenuItem, Categories categories)
    {
        menuItems.Name = newMenuItem.Name;
        menuItems.Category = categories;
        menuItems.SellingPrice = newMenuItem.SellingPrice;
        menuItems.ImageUrl = newMenuItem.ImageUrl;
        menuItems.CategoryId = newMenuItem.CategoryId;
        menuItems.IsAvailable = newMenuItem.IsAvailable;
        menuItems.IsVegetarian = newMenuItem.IsVegetarian;
        menuItems.Description = newMenuItem.Description;
        menuItems.Spicy = newMenuItem.Spicy;
        menuItems.CostPrice = newMenuItem.CostPrice;
    }

    public static ReadMenuItemForRating ToReadMenuItemForRating(this MenuItems menuItems)
    {
        return new ReadMenuItemForRating()
        {
            Id = menuItems.MenuItemId,
            Name = menuItems.Name,
            ImageUrl = menuItems.ImageUrl,
            SellingPrice = menuItems.SellingPrice
        };
    }

    public static ReadMenuItemForOrderItemDto ToReadMenuItemForOrderItems(this MenuItems menuItems)
    {
        return new ReadMenuItemForOrderItemDto()
        {
            Id = menuItems.MenuItemId,
            Name = menuItems.Name,
            Description = menuItems.Description,
            IsVegetarian = menuItems.IsVegetarian,
        };
    }
}