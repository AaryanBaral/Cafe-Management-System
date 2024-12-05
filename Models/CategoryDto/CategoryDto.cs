namespace Cafe_Management_System.Models.Category;

public class AddCategoryDto
{
    public required string CategoryName { get; set; }
    public required string Description { get; set; }
    
}

public class ReadCategoryDto
{
    public required string CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public required string Description { get; set; }
}