namespace Cafe_Management_System.Models.ResponseDto;

public class ResponseDto<T>
{
       public required T Data { get; set; }
       public bool Success { get; set; } =  true;
       
}