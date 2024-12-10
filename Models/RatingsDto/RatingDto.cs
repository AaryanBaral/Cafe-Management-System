using Cafe_Management_System.Enums;
using Cafe_Management_System.Models.MenuItemDto;
using Cafe_Management_System.Models.OrderDto;
using Cafe_Management_System.Models.UsersDto;

namespace Cafe_Management_System.Models.RatingsDto;

public class AddRatingDto
{
    public required string CustomerId { get; set; }
    public required string MenuItemId { get; set; }
    public required RatingEnum RatingValue { get; set; }
}
public class UpdateRatingDto
{
    public required RatingEnum RatingValue { get; set; }
}

public class ReadRatingDto
{
    public required ReadUserDto User { get; set; }
    public required ReadMenuItemForRating MenuItem { get; set; }
    public required RatingEnum RatingValue { get; set; }
}
