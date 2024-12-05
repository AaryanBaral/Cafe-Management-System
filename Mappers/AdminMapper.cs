using Cafe_Management_System.Entities;
using Cafe_Management_System.Migrations;
using Cafe_Management_System.Models.AdminDto;

namespace Cafe_Management_System.Mappers;

public static class AdminMapper
{
    public static Admins ToAdmin(this RegisterAdminDto adminDto)
    {
        return new Admins()
        {
            Email = adminDto.Email,
            UserName = adminDto.UserName,
        };
    }
    public static ReadAdminDto ToReadAdminDto(this Admins admin)
    {
        return new ReadAdminDto()
        {
            Id = admin.Id,
            Email = admin.Email,
            UserName = admin.UserName,
            ImageUrl = admin.ImageUrl
        };
    }
    
    public static Admins UpdateAdmin(this Admins user, RegisterAdminDto registerAdmin)
    {
        user.Email = registerAdmin.Email;
        user.UserName = registerAdmin.UserName;
        return user;
    }
}