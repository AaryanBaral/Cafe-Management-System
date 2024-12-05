using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Cafe_Management_System.Services.CloudinaryService;

public class CloudinaryService:ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> UploadImage(IFormFile file)
    {
        var imageId = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var uploadParameters = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            PublicId = imageId
        };
        var uploadResults = await _cloudinary.UploadAsync(uploadParameters);
        var imagePublicId = uploadResults.PublicId;
        return uploadResults.SecureUrl.ToString();
    }

    public async Task<bool> DeleteImageByPublicId(string publicId)
    {
        await _cloudinary.DestroyAsync(new DeletionParams(publicId));
        return true;
    }
}