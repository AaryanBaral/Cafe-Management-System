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
    
    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> CreateUsers([FromForm]RegisterUserDto registerUserDto, IFormFile? image)
    {
            var users = await _repository.RegisterUser(registerUserDto, image);
            return Ok( new ResponseDto<string>()
            {
                Data = users 
            });   
    }

    [HttpGet]
    [Route("/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var result = await _repository.GetUserById(id);
        return Ok(new ResponseDto<ReadUserDto>()
        {
            Data = result
        });
        
    }

    [HttpPut]
    [Route("/{id}")]
    public async Task<IActionResult> UpdateUser(string id, RegisterUserDto registerUserDto, IFormFile? image)
    {
        await _repository.UpdateUser( registerUserDto, id, image);
        return Ok(new ResponseDto<string>()
        {
            Data = $"User successfully updated"
        });
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        await _repository.DeleteUserById(id);
        return Ok(new ResponseDto<string>()
        {
            Data = $"User successfully deleted"
        });
    }
}