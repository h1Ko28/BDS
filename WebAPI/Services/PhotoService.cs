using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        public PhotoService(IConfiguration configuration) {
            Account account = new Account(
                // configuration.GetSection("CloudinarySettings:CloudName").Value,
                // configuration.GetSection("CloudinarySettings:APIKey").Value,
                // configuration.GetSection("CloudinarySettings:APISecret").Value
                "h1ko",
                "324819691383848",
                "41qXYmtEL7GIoVLk75xn_OW2P_U"
            );

            cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile photo)
        {
            var upload = new ImageUploadResult();

            if (photo.Length > 0) {
                using var stream = photo.OpenReadStream();
                var uploadParam = new ImageUploadParams{
                    File = new FileDescription(photo.FileName, stream),
                    Transformation = new Transformation()
                        .Height(500).Width(800)
                };
                upload = await cloudinary.UploadAsync(uploadParam);
            }
            return upload;
        }
    }
}