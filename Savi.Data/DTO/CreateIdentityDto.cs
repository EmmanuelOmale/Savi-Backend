using Microsoft.AspNetCore.Http;

namespace Savi.Data.DTO
{
	public class CreateIdentityDto
	{
		public string Name { get; set; }
		public string IdentificationNumber { get; set; }
		public IFormFile DocumentImage { get; set; }

		public string? DocumentImageUrl { get; set; }
	}
}