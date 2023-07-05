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

        public async Task<DocumentUploadResult> UploadFileAsync(string documentContent)
        {
            if (string.IsNullOrEmpty(documentContent))
            {
                throw new ArgumentException("Invalid document content");
            }

            byte[] documentBytes = Encoding.UTF8.GetBytes(documentContent);
            using var documentStream = new MemoryStream(documentBytes);

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription("document.txt", documentStream),
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
                Length = documentBytes.Length
            };
        }

    }
}
