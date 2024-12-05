using Cafe_Management_System.Entities;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.AdminDto;
using Cafe_Management_System.Services.CloudinaryService;
using Cafe_Management_System.Services.JwtService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Repositories.Admin;

public class AdminRepository(
    UserManager<Admins> adminManager, 
    CloudinaryService cloudinaryService,
    JwtService jwtService
    ):IAdminRepository
{
    private readonly UserManager<Admins> _adminManager = adminManager;
    private readonly CloudinaryService _cloudinaryService = cloudinaryService;
    private readonly JwtService _jwtService = jwtService;

    public async Task<string> AddAdmin(RegisterAdminDto registerAdminDto, IFormFile? image)
    {
        var admin = registerAdminDto.ToAdmin();
        var isCreated = await _adminManager.CreateAsync(admin, registerAdminDto.Password);
        if (image is not null)
        {
            var downloadUrl = await _cloudinaryService.UploadImage(image);
            admin.ImageUrl = downloadUrl;
        }
        if(!isCreated.Succeeded) throw new ApplicationException("Failed to create admin");
        var token = _jwtService.GenerateJwtToken(admin);
        return token;
    }
    
    public async Task<string> LoginAdmin(LoginAdminDto loginAdminDto)
    {
        var admin = await _adminManager.FindByEmailAsync(loginAdminDto.Email) ?? throw new AuthenticationFailureException("Email Not Found");
        var isPasswordCorrect = await _adminManager.CheckPasswordAsync(admin, loginAdminDto.Password);
        if (!isPasswordCorrect) throw new AuthenticationFailureException("Password Not Correct");
        var token = _jwtService.GenerateJwtToken(admin);
        return token;
    }
    
    public async Task<ReadAdminDto> GetAdminByEmail(string email)
    {
        var admin = await _adminManager.FindByEmailAsync(email) ?? throw new KeyNotFoundException("Email Not Found");
        return admin.ToReadAdminDto();
    }

    public async Task<List<ReadAdminDto>> GetAllAdmins()
    {
        var admins = await _adminManager.Users.ToListAsync();
        var readAdminsList = admins.Select(u => u.ToReadAdminDto()).ToList();
        return readAdminsList;
    }

    public async Task<ReadAdminDto> GetAdminById(string id)
    {
        var admin = await _adminManager.FindByIdAsync(id)?? throw new KeyNotFoundException("Admin Not Found");
        return admin.ToReadAdminDto();
    }

    public async Task DeleteAdminById(string id)
    {
        var admin = await _adminManager.FindByIdAsync(id) ?? throw new KeyNotFoundException("Admin Not Found");
        var result = await _adminManager.DeleteAsync(admin);
        if(admin.ImageUrl is not null)
            await _cloudinaryService.DeleteImageByPublicId(admin.ImageUrl);
        if(!result.Succeeded) throw new ApplicationException($"Internal Server Error: {result.Errors}");
    }

    public async Task UpdateAdmin(RegisterAdminDto registerAdminDto, string id, IFormFile? image)
    {
        var existingAdmin = await _adminManager.FindByIdAsync(id)?? throw new KeyNotFoundException("Admin Not Found");
        var updatedAdmin = existingAdmin.UpdateAdmin(registerAdminDto);
        if (image is null)
        {
            await _adminManager.UpdateAsync(updatedAdmin);
            return;
        }
        if (updatedAdmin.ImageUrl is not null)
        {
            await _cloudinaryService.DeleteImageByPublicId(updatedAdmin.ImageUrl);
        }

        updatedAdmin.ImageUrl = await _cloudinaryService.UploadImage(image);
        await _adminManager.UpdateAsync(updatedAdmin);
    }
    
    // Email confirmation remains
    
}