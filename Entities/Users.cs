using Cafe_Management_System.Enums;
using Microsoft.AspNetCore.Identity;

namespace Cafe_Management_System.Entities;

public class Users:IdentityUser
{
    public override string? PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public override required string Email { get; set; }
    public override required string UserName { get; set; }
    public required AuthType AuthType { get; set; }
    public string? AuthId { get; set; }
    
    public string? ImageUrl { get; set; }
}


