using Cafe_Management_System.Models.UsersDto;

namespace Cafe_Management_System.Repositories.User;

public interface IUserRepository
{
    Task<string> RegisterUser(RegisterUserDto registerUserDto, IFormFile? file);
    Task<string> LoginUser(LoginUserDto loginUserDto);
    Task<ReadUserDto> GetUserByEmail(string email);
    Task<List<ReadUserDto>> GetAllUsers();
    Task<ReadUserDto> GetUserById(string id);
    Task DeleteUserById(string id);
    Task UpdateUser(RegisterUserDto registerUserDto, string id, IFormFile? image);

}