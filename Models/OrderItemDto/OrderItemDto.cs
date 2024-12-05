using Cafe_Management_System.Models.MenuItemDto;

namespace Cafe_Management_System.Models.OrderItemDto;

public class AddOrderItemDto
{
    public required string MenuItemId { get; set; }
    public int Quantity { get; set; }
    
}

public class ReadOrderItemDto
{
    public required string OrderItemId { get; set; }
    public required string OrderId { get; set; }
    public required ReadMenuItemForOrderItemDto MenuItem { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double SubTotal { get; set; }
    
}