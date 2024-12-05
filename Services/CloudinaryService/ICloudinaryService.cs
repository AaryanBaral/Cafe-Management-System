namespace Cafe_Management_System.Services.CloudinaryService;

public interface ICloudinaryService
{
    Task<string> UploadImage(IFormFile file);
    Task<bool> DeleteImageByPublicId(string publicId);
}
