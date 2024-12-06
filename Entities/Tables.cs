namespace Cafe_Management_System.Entities;

public class Tables
{
    public string TableId { get; set; } = Guid.NewGuid().ToString();
    public required string TableNumber { get; set; }
    public required bool IsOccupied { get; set; } 
    public required string QrData { get; set; }
    
    public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    
}