namespace Cafe_Management_System.Models.UsersDto;

public class UserDto
{
    
}

public class RegisterUserDto
{
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public string? ImageUrl { get; set; }
    public required string Password { get; set; }
}

public class LoginUserDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class ReadUserDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public string? ImageUrl { get; set; }
}