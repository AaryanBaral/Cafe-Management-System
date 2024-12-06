using Cafe_Management_System.Models.OrderDto;

namespace Cafe_Management_System.Repositories.Order;

public interface IOrderRepository
{
    Task CreateOrder(CreateOrderDto createOrderDto);
    Task<ReadOrderDto> GetOrder(string orderId);
    Task DeleteOrder(string orderId);
    Task<List<ReadOrderDto>> GetAllOrderByTableId(string tableId);
    Task<List<ReadOrderDto>> GetAllOrderByCustomerId(string customerId);
}