using Cafe_Management_System.Enums;

namespace Cafe_Management_System.Entities;

public class MenuItems

{
    public string MenuItemId { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set;}
    public required string Description { get; set;}
    public required double SellingPrice { get; set;}
    public required double CostPrice { get; set;}
    public required string ImageUrl { get; set;}
    public required string CategoryId { get; set;}
    public required Categories Category { get; set;}
    public required bool IsAvailable { get; set;}
    public required bool IsVegetarian{ get; set;}
    public required SpicyEnum Spicy{ get; set;}
    
}