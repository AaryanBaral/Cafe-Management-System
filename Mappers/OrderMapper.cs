using Cafe_Management_System.Entities;
using Cafe_Management_System.Enums;
using Cafe_Management_System.Models.OrderDto;
using Cafe_Management_System.Models.OrderItemDto;

namespace Cafe_Management_System.Mappers;

public static class OrderMapper
{
    public static Orders ToOrder(this CreateOrderDto orderDt, Tables table, Users customer)
    {
        return new Orders()
        {
            Table = table,
            TableId = orderDt.TableId,
            Customer = customer,
            CustomerId = orderDt.CustomerId,
            TotalAmount = orderDt.TotalAmount,
            OrderStatus = OrderStatusEnum.Pending,
            PaymentStatus = PaymentStatusEnum.Pending,
        };
    }

    public static ReadOrderDto ToReadOrder(this Orders order, Tables table, Users customer, List<ReadOrderItemDto> readOrderItems)
    {
        return new ReadOrderDto()
        {
            Table = table.ToReadTable(),
            TotalAmount = order.TotalAmount,
            OrderUser = customer.ToReadUserDto(),
            PaymentStatus = order.PaymentStatus,
            PaymentMethod = order.PaymentMethod,
            OrderItems = readOrderItems
        };
    }
    
}