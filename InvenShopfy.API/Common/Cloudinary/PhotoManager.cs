using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace InvenShopfy.API.Common.Cloudinary;

// public class PhotoManager : IPhotoService
// {
//     private readonly CloudinaryDotNet.Cloudinary _cloudinary;
//     private readonly string _folder;
//
//     public PhotoManager(IConfiguration configuration)
//     {
//         var cloudinarySettings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
//         Account account = new Account(
//             cloudinarySettings?.CloudName,
//             cloudinarySettings?.ApiKey,
//             cloudinarySettings?.ApiSecret
//         );
//         _cloudinary = new CloudinaryDotNet.Cloudinary(account);
//         _folder = cloudinarySettings.Folder;
//     }
//
//     public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
//     {
//         var uploadResult = new ImageUploadResult();
//         if (file.Length > 0)
//         {
//             using (var stream = file.OpenReadStream())
//             {
//                 var uploadParams = new ImageUploadParams
//                 {
//                     File = new FileDescription(file.FileName, stream),
//                     Folder = _folder
//                 };
//                 uploadResult = await _cloudinary.UploadAsync(uploadParams);
//             }
//         }
//         return uploadResult;
//     }
// }