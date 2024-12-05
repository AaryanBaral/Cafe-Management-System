using Cafe_Management_System.Enums;

namespace Cafe_Management_System.Models.RatingsDto;

public class AddRatingDto
{
    public required string CustomerId { get; set; }
    public required string MenuItemId { get; set; }
    public required string OrderId { get; set; }
    public required RatingEnum RatingValue { get; set; }
}