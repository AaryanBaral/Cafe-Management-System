namespace Cafe_Management_System.Entities;

public class Categories
{
    public string CategoryId { get; set; } = Guid.NewGuid().ToString();
    public required string CategoryName { get; set; } 
    public required string CategoryDescription { get; set; }
}