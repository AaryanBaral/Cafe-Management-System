using Cafe_Management_System.Models.OrderDto;

namespace Cafe_Management_System.Repositories.Order;

public interface IOrderRepository
{
    Task CreateOrder(CreateOrderDto createOrderDto);
}