using Cafe_Management_System.Entities;
using Cafe_Management_System.Enums;
using Cafe_Management_System.Models.UsersDto;

namespace Cafe_Management_System.Mappers;

public static  class UserMapper
{
    public static ReadUserDto ToReadUserDto(this Users user)
    {
        return new ReadUserDto()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            ImageUrl = user.ImageUrl
        };
    }

    public static Users ToUser(this RegisterUserDto user, AuthType authType)
    {
        return new Users()
        {
            Email = user.Email,
            UserName = user.UserName,
            AuthType = authType
        };
    }

    public static Users UpdateUser(this Users user,RegisterUserDto registerUser)
    {
        user.Email = registerUser.Email;
        user.UserName = registerUser.UserName;
        return user;
    }
    
}
