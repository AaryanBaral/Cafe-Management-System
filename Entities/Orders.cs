
using Cafe_Management_System.Enums;

namespace Cafe_Management_System.Entities;
public class Orders
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public required string TableId {get; set;}
    public required Tables Table {get; set;}
    public required string CustomerId {get; set;}
    public required Users Customer {get; set;}
    public required double TotalAmount {get; set;}
    public required OrderStatusEnum OrderStatus {get; set;}
    public required PaymentStatusEnum PaymentStatus {get; set;}
    public PaymentMethodEnum PaymentMethod {get; set;}
    public  Payments? Payment { get; set; }
    public string? PaymentId { get; set; }
    public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
}


