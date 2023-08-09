using Microsoft.AspNetCore.Mvc;
using Savi.Api.Service;

namespace Savi.Api.Controllers
{
	[ApiController]
	[Route("api/google/signup")]
	public class GoogleSignupController : ControllerBase
	{
		private readonly IGoogleSignupService _googleSignupService;

		public GoogleSignupController(IGoogleSignupService googleSignupService)
		{
			_googleSignupService = googleSignupService;
		}

		[HttpPost]
		public async Task<IActionResult> SignupWithGoogle(string code)
		{
			try
			{
				var userProfile = await _googleSignupService.SignupWithGoogleAsync(code);

				if (userProfile != null && userProfile.Email == code)
				{
					return Ok(userProfile);
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}