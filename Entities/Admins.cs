using Microsoft.AspNetCore.Identity;

namespace Cafe_Management_System.Entities;

public class Admins:IdentityUser
{
    public string? ImageUrl { get; set; }
    
    public override required string UserName { get; set; }
    public override required string Email { get; set; }
    
}