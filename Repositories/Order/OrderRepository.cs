using System.ComponentModel.DataAnnotations;
using Cafe_Management_System.Data;
using Cafe_Management_System.Entities;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.OrderDto;
using Cafe_Management_System.Repositories.OrderItem;
using Cafe_Management_System.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Repositories.Order;

public class OrderRepository(
    AppDbContext context,
    IOrderItemRepository orderItemRepository,
    UserManager<Users> userManager
    )
{
    private readonly AppDbContext _context = context;
    private readonly IOrderItemRepository _orderItemRepository=orderItemRepository;
    private readonly UserManager<Users> _userManager=userManager;
    private static  bool AreEqual(double a, double b, double epsilon = 1e-8) => Math.Abs(a - b) < epsilon;
    private async Task ValidateOrder(CreateOrderDto createOrderDto)
    {
        var total = (await Task.WhenAll(createOrderDto.AddOrderItem.Select(async e =>
        {
            var menuItem = await _context.MenuItems.FindAsync(e.MenuItemId) ??
                           throw new KeyNotFoundException("MenuItem Not Found");
            return e.Quantity * menuItem.SellingPrice;
        }))).Sum();
        if(!AreEqual(total, createOrderDto.TotalAmount)) throw new ValidationException("Total amount is not accurate");

    }
    public async Task CreateOrder(CreateOrderDto createOrderDto)
    {
       await ValidateOrder(createOrderDto);
       var table = await _context.Tables.FindAsync(createOrderDto.TableId)??throw new KeyNotFoundException("Table Not Found");
       var user = await _userManager.FindByIdAsync(createOrderDto.CustomerId)?? throw new KeyNotFoundException("Customer Not Found");
       var order = createOrderDto.ToOrder(table, user);
       _context.Orders.Add(order);
       await _context.SaveChangesAsync();
       foreach (var orderItem in createOrderDto.AddOrderItem)
       {
           await _orderItemRepository.AddOrderItem(orderItem, order.OrderId);
       }
       await _context.SaveChangesAsync();
    }

    public async Task DeleteOrder(string orderId)
    {
        var order = await _context.Orders
            .Include(o=>o.OrderItems)
            .FirstOrDefaultAsync(o=>o.OrderId==orderId)??throw new KeyNotFoundException("Order Not Found");
        await _orderItemRepository.DeleteAllOrderItem(order.OrderId);
        await _context.Orders.Where(o => o.OrderId == orderId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
    
}