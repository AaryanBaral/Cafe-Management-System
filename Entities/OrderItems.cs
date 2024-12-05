namespace Cafe_Management_System.Entities;

public class OrderItems
{
    public string OrderItemId {get; set;} = Guid.NewGuid().ToString();
    public required string OrderId { get; set; }
    public required Orders Order {get; set;}
    public required string MenuItemId { get; set; }
    public required MenuItems MenuItem { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double SubTotal { get; set; }
}