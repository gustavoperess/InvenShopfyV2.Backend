using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InvenShopfy.Core;
using Microsoft.Extensions.Options;

namespace InvenShopfy.API.Common.CloudinaryServiceNamespace;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> UploadImageAsync(string base64Image, string folderName)
    {
        var base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1); 
        var imageBytes = Convert.FromBase64String(base64Data);
        using (var stream = new MemoryStream(imageBytes))
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription("file", stream),
                Folder = folderName
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult;
            }
            else
            {
                throw new Exception("Image upload failed!");
            }
        }
    }
}
