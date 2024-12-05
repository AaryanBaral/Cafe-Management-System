using Cafe_Management_System.Entities;
using Cafe_Management_System.Models.RatingsDto;

namespace Cafe_Management_System.Mappers;

public static class RatingMapper
{
    public static Ratings ToRating(this AddRatingDto rating, Users user, MenuItems menuItem, Orders order)
    {
        return new Ratings()
        {
            CustomerId = user.Id,
            Customer = user,
            MenuItem = menuItem,
            MenuItemId = menuItem.MenuItemId,
            Order = order,
            OrderId = order.OrderId,
            RatingValue = rating.RatingValue,
        };
    }
}