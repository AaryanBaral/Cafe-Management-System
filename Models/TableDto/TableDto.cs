namespace Cafe_Management_System.Models.TableDto;

public class AddTableDto
{
    public string TableNo { get; set; }
    
}

public class ReadTableDto
{
    public required string TableId { get; set; }
    public required string TableNo { get; set; }
}