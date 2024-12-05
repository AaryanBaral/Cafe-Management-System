using Cafe_Management_System.Entities;
using Cafe_Management_System.Models.MenuItemDto;
using Cafe_Management_System.Models.OrderItemDto;

namespace Cafe_Management_System.Mappers;

public static class OrderItemMapper
{
    public static OrderItems ToOrderItems(this AddOrderItemDto addOrderItemDto, Orders order, MenuItems menuItem)
    {
        return new OrderItems()
        {
            OrderId = order.OrderId,
            Order = order,
            MenuItemId = addOrderItemDto.MenuItemId,
            Quantity = addOrderItemDto.Quantity,
            UnitPrice = menuItem.SellingPrice,
            MenuItem = menuItem,
            SubTotal = menuItem.SellingPrice * addOrderItemDto.Quantity
        };
    }

    public static ReadOrderItemDto ToReadOrderItemDto(this OrderItems orderItem)
    {
        return new ReadOrderItemDto()
        {
            OrderId = orderItem.OrderId,
            OrderItemId = orderItem.OrderItemId,
            Quantity = orderItem.Quantity,
            SubTotal = orderItem.SubTotal,
            UnitPrice = orderItem.UnitPrice,
            MenuItem = new ReadMenuItemForOrderItemDto()
            {
                Id = orderItem.MenuItemId,
                Name = orderItem.MenuItem.Name,
                Description = orderItem.MenuItem.Description,
                IsVegetarian = orderItem.MenuItem.IsVegetarian,
            }
        };
    }
}