using Microsoft.AspNetCore.Identity;

namespace Cafe_Management_System.Services.JwtService;

public interface IJwtService
{
    string GenerateJwtToken(IdentityUser user);
    
}