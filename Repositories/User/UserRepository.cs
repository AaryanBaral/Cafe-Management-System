using Cafe_Management_System.Entities;
using Cafe_Management_System.Enums;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.UsersDto;
using Cafe_Management_System.Services.CloudinaryService;
using Cafe_Management_System.Services.JwtService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Repositories.User;

public class UserRepository(
    UserManager<Users> userManager, 
    ICloudinaryService cloudinaryService,
    IJwtService jwtService
    ):IUserRepository
{
    private readonly UserManager<Users> _userManager = userManager;
    private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
    private readonly IJwtService _jwtService = jwtService;
    
    public async Task<string> RegisterUser(RegisterUserDto registerUserDto, IFormFile? file )
    {
        
        var newUser = new Users( )
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.Email,
            AuthType = AuthType.Manual
        };
        if (file is not null)
        {
            var downloadUrl = await _cloudinaryService.UploadImage(file);
            newUser.ImageUrl = downloadUrl;
        }
        var isUserCreated = await _userManager.CreateAsync(newUser, registerUserDto.Password)?? throw new Exception("Error while processing identity");
        if (!isUserCreated.Succeeded)
        {
            throw new Exception(isUserCreated.Errors.FirstOrDefault()?.Description);
        }
        var token = _jwtService.GenerateJwtToken(newUser);
        return token;
    }

    public async Task<string> LoginUser(LoginUserDto loginUserDto)
    {
        var user = await _userManager.FindByEmailAsync(loginUserDto.Email) ?? throw new AuthenticationFailureException("Email Not Found");
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
        if (!isPasswordCorrect) throw new AuthenticationFailureException("Password Not Correct");
        var token = _jwtService.GenerateJwtToken(user);
        return token;
    }
    
    public async Task<ReadUserDto> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ?? throw new KeyNotFoundException("Email Not Found");
        return user.ToReadUserDto();
    }

    public async Task<List<ReadUserDto>> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var readUserList = users.Select(u => u.ToReadUserDto()).ToList();
        return readUserList;
    }

    public async Task<ReadUserDto> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id)?? throw new KeyNotFoundException("User Not Found");
        return user.ToReadUserDto();
    }

    public async Task DeleteUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id) ?? throw new KeyNotFoundException("User Not Found");
        var result = await _userManager.DeleteAsync(user);
        if(user.ImageUrl is not null)
            await _cloudinaryService.DeleteImageByPublicId(user.ImageUrl);
        if(!result.Succeeded) throw new ApplicationException($"Internal Server Error: {result.Errors}");
    }

    public async Task UpdateUser(RegisterUserDto registerUserDto, string id, IFormFile? image)
    {
        var existingUser = await _userManager.FindByIdAsync(id)?? throw new KeyNotFoundException("User Not Found");
        var updatedUser = existingUser.UpdateUser(registerUserDto);
        if (image is null)
        {
            await _userManager.UpdateAsync(updatedUser);
            return;
        }
        if (updatedUser.ImageUrl is not null)
        {
            await _cloudinaryService.DeleteImageByPublicId(updatedUser.ImageUrl);
        }

        updatedUser.ImageUrl = await _cloudinaryService.UploadImage(image);
        await _userManager.UpdateAsync(updatedUser);
    }
    
    // Email confirmation remains
}