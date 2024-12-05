using Cafe_Management_System.Enums;

namespace Cafe_Management_System.Entities;

public class Payments
{
    public required string PaymentId { get; set; } = Guid.NewGuid().ToString();
    public required string OrderId { get; set; }
    public required Orders Order { get; set; }
    public required double  Amount { get; set; }
    public required PaymentMethodEnum PaymentMethod { get; set; }
    public required DateTime PaymentDate { get; set; } = DateTime.Now;
    public required PaymentStatusEnum PaymentStatus { get; set; }
}
