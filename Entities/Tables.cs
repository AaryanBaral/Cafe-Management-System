using System.ComponentModel.DataAnnotations;

namespace Cafe_Management_System.Entities;

public class Tables
{
    public string TableId { get; set; } = Guid.NewGuid().ToString();
    public required string TableNumber { get; set; }
    public required bool IsOccupied { get; set; } 
    public required string Qrdata { get; set; }
    
}