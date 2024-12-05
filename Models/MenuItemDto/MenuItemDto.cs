using Cafe_Management_System.Enums;
using Cafe_Management_System.Models.Category;

namespace Cafe_Management_System.Models.MenuItemDto;

public class AddMenuItemDto
{
    public required string Name { get; set;}
    public required string Description { get; set;}
    public required double SellingPrice { get; set;}
    public required double CostPrice { get; set;}
    public required string ImageUrl { get; set;}
    public required string CategoryId { get; set;}
    public required bool IsAvailable { get; set;}
    public required bool IsVegetarian{ get; set;}
    public required SpicyEnum Spicy{ get; set;}
}

public class ReadMenuItemDto
{
    public required string Id { get; set;}
    public required string Name { get; set;}
    public required string Description { get; set;}
    public required double SellingPrice { get; set;}
    public required string ImageUrl { get; set;}
    public required string CategoryId { get; set;}
    public required bool IsAvailable { get; set;}
    public required bool IsVegetarian{ get; set;}
    public required SpicyEnum Spicy{ get; set;} 
    public required ReadCategoryDto Category { get; set;}
}

public class ReadMenuItemForOrderItemDto
{
    public required string Id { get; set;}
    public required string Name { get; set;}
    public required string Description { get; set;}
    public required bool IsVegetarian{ get; set;}
}