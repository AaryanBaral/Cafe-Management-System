using Cafe_Management_System.Entities;
using Cafe_Management_System.Enums;
using Cafe_Management_System.Models.OrderItemDto;
using Cafe_Management_System.Models.TableDto;
using Cafe_Management_System.Models.UsersDto;

namespace Cafe_Management_System.Models.OrderDto;

public class CreateOrderDto
{
    public required string TableId {get; set;}
    public required string CustomerId {get; set;}
    public required double TotalAmount {get; set;}
    public required List<AddOrderItemDto> AddOrderItem {get; set;}
}

public class ReadOrderDto
{
    public required string OrderId {get; set;}
    public required ReadTableDto Table {get; set;}
    public required double TotalAmount {get; set;}
    public required List<ReadOrderItemDto> OrderItems {get; set;}
    public required ReadUserDto OrderUser  {get; set;}
    public required PaymentStatusEnum PaymentStatus {get; set;}
    public PaymentMethodEnum PaymentMethod {get; set;}
    public Payments? Payment {get; set;}
    
}
