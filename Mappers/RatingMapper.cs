using Cafe_Management_System.Entities;
using Cafe_Management_System.Models.RatingsDto;

namespace Cafe_Management_System.Mappers;

public static class RatingMapper
{
    public static Ratings ToRating(this AddRatingDto rating, Users user, MenuItems menuItem)
    {
        return new Ratings()
        {
            CustomerId = user.Id,
            Customer = user,
            MenuItem = menuItem,
            MenuItemId = menuItem.MenuItemId,
            RatingValue = rating.RatingValue,
        };
    }

    public static void UpdateRating(this Ratings rating, UpdateRatingDto updateRatingDto)
    {
        rating.RatingValue = updateRatingDto.RatingValue;
    }

    public static ReadRatingDto ToReadRatingDto(this Ratings rating, Users user, MenuItems menuItem)
    {
        return new ReadRatingDto()
        {
            MenuItem = menuItem.ToReadMenuItemForRating(),
            RatingValue = rating.RatingValue,
            User = user.ToReadUserDto()
        };
    }
}