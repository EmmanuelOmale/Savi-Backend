using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Core.Services
{
    public class DocumentUploadService : IDocumentUploadService
    {
        private readonly Cloudinary _cloudinary;

        public DocumentUploadService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<DocumentUploadResult> UploadFileAsync(IFormFile file, string fileType)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Type = fileType,
                UseFilename = true,
                UniqueFilename = false
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                var errorDetails = new ErrorDetails
                {
                    Message = uploadResult.Error.Message,
                };

                throw new Exception(errorDetails.ToString());
            }

            return new DocumentUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString(),
                SecureUrl = uploadResult.SecureUrl.ToString(),
                Format = uploadResult.Format,
                Length = file.Length
            };
        }
        
    }
}
