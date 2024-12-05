using Cafe_Management_System.Models.OrderItemDto;

namespace Cafe_Management_System.Repositories.OrderItem;

public interface IOrderItemRepository
{
    Task AddOrderItem(AddOrderItemDto addOrderItemDto, string orderId);
    Task<List<ReadOrderItemDto>> GetAllOrderItems(string orderId);
    Task DeleteAllOrderItem(string orderId);
    Task<ReadOrderItemDto> GetOrderItemById(string orderItemId);

}