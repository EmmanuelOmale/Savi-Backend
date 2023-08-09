using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Savi.Data.DTO
{
	public class UpdateIdentityDto
	{
		[Display(Name = "Identification Type")]
		public string Name { get; set; }

		public string IdentificationNumber { get; set; }

		public IFormFile DocumentImage { get; set; }

		public string? DocumentImageUrl { get; set; } = string.Empty;
	}
}