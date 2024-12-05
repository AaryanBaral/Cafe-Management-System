namespace Cafe_Management_System.Models.AdminDto;

public class RegisterAdminDto
{
    public string? ImageUrl { get; set; }
    
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    
}

public class LoginAdminDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class ReadAdminDto
{
    public required string Id { get; set; }
    public string? ImageUrl { get; set; }
    
    public required string UserName { get; set; }
    public required string Email { get; set; }
    
}