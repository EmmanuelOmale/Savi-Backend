using Microsoft.AspNetCore.Http;
using Savi.Data.Domains;

namespace Savi.Core.Interfaces
{
	public interface IDocumentUploadService
	{
		Task<DocumentUploadResult> UploadFileAsync(string documentContent);

		Task<DocumentUploadResult> UploadImageAsync(IFormFile image);
	}
}