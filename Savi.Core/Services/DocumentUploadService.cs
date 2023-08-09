using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using System.Text;

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

		public async Task<DocumentUploadResult> UploadImageAsync(IFormFile image)
		{
			if (image == null || image.Length == 0)
			{
				throw new ArgumentException("Invalid image");
			}

			// Convert the image to bytes
			byte[] imageBytes;
			using (var memoryStream = new MemoryStream())
			{
				await image.CopyToAsync(memoryStream);
				imageBytes = memoryStream.ToArray();
			}

			// Upload the image using Cloudinary
			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(image.FileName, new MemoryStream(imageBytes)),
				// Set other upload parameters as needed
			};

			var uploadResult = await _cloudinary.UploadAsync(uploadParams);

			if (uploadResult.Error != null)
			{
				throw new Exception(uploadResult.Error.Message);
			}

			return new DocumentUploadResult
			{
				PublicId = uploadResult.PublicId,
				Url = uploadResult.Url.ToString(),
				SecureUrl = uploadResult.SecureUrl.ToString(),
				Format = uploadResult.Format,
				Length = image.Length
			};
		}
	}
}