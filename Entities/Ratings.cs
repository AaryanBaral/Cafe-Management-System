using System.ComponentModel.DataAnnotations;
using Cafe_Management_System.Enums;

namespace Cafe_Management_System.Entities;

public class Ratings
{
    public string RatingId { get; set; } = Guid.NewGuid().ToString();
    
    public required string CustomerId { get; set; }
    public required Users Customer { get; set; }
    public required string MenuItemId { get; set; }
    public required MenuItems MenuItem { get; set; }
    public required Orders Order { get; set; }
    public required string OrderId { get; set; }
    
    public required RatingEnum RatingValue { get; set; }
    
    public  DateTime RatedAt { get; set; } = DateTime.Now;
}