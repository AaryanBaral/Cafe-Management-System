using Cafe_Management_System.Models.ResponseDto;
using Cafe_Management_System.Models.UsersDto;
using Cafe_Management_System.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserRepository repository
    ):ControllerBase
{
    private readonly IUserRepository _repository = repository;
    
    
    [HttpGet]
    [Route("/")]
    public async Task<IActionResult> GetAllUsers()
    {
            var users = await _repository.GetAllUsers();
            return StatusCode(StatusCodes.Status200OK, new ResponseDto<List<ReadUserDto>>()
            {
                Data = users,
                Success = true,
            });   
    }
    [HttpGet]
    [Route("/register")]
    public async Task<IActionResult> CreateUsers(RegisterUserDto registerUserDto, IFormFile? image)
    {
            var users = await _repository.RegisterUser(registerUserDto, image);
            return StatusCode(StatusCodes.Status200OK, new ResponseDto<string>()
            {
                Data = users,
                Success = true,
            });   
    }
    
}