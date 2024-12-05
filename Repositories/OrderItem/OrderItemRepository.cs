using Cafe_Management_System.Data;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.OrderItemDto;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Repositories.OrderItem;

public class OrderItemRepository(
    AppDbContext context
    ):IOrderItemRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddOrderItem(AddOrderItemDto addOrderItemDto, string orderId)
    {
        var menuItem = await _context.MenuItems.FindAsync(addOrderItemDto.MenuItemId)?? throw new KeyNotFoundException("MenuItem Not Found");
        var order = await _context.Orders.FindAsync(orderId)?? throw new KeyNotFoundException("Order Not Found");
        var newOrder = addOrderItemDto.ToOrderItems(order, menuItem);
        _context.OrderItems.Add(newOrder);
        await _context.SaveChangesAsync();
    }
    

    public async Task<List<ReadOrderItemDto>> GetAllOrderItems(string orderId)
    {
        var order = await _context.Orders.FindAsync(orderId)?? throw new KeyNotFoundException("Order Not Found");
        var orderItems = await _context.OrderItems
            .Include(oi => oi.MenuItem)
            .Where(o=>o.OrderId == orderId)
            .ToListAsync() ?? throw new KeyNotFoundException("Order Not Found");
        return orderItems.Select(e => e.ToReadOrderItemDto()).ToList();
    }

    public async Task DeleteAllOrderItem(string orderId)
    {
        var order = await _context.Orders
            .Include(o=>o.OrderItems)
            .FirstOrDefaultAsync(o=>o.OrderId == orderId)?? throw new KeyNotFoundException("Order Not Found");
        
        _context.OrderItems.RemoveRange(order.OrderItems);
        await _context.SaveChangesAsync();
    }
    
    public async Task<ReadOrderItemDto> GetOrderItemById(string orderItemId)
    {
        var order = await _context.OrderItems.FindAsync(orderItemId)?? throw new KeyNotFoundException("Order Not Found");
        return order.ToReadOrderItemDto();
    }
    
    
}