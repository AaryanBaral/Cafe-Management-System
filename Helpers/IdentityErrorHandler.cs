using Microsoft.AspNetCore.Identity;

namespace Cafe_Management_System.Helpers;

public class IdentityErrorHandler
{
    public static List<string> GetErrors(IdentityResult result)
    {
        return result.Errors.Select(error => $"{error.Code}: {error.Description}").ToList();
    }
}