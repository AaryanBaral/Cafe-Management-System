using System.ComponentModel.DataAnnotations;

namespace Cafe_Management_System.Entities;

public class Ratings
{
    public required string RatingId { get; set; } = Guid.NewGuid().ToString();
    
    public required string CustomerId { get; set; }
    public required Users Customer { get; set; }
    public required string MenuItemId { get; set; }
    public required MenuItems MenuItem { get; set; }
    public required Orders Orders { get; set; }
    public required string OrderId { get; set; }
    
    public required DateTime RatedAt { get; set; } = DateTime.Now;
}