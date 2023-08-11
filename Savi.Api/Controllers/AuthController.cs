﻿using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authenticationService;

		public AuthController(IAuthService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> Register([FromBody] SignUpDto model)
		{
			if(ModelState.IsValid)
			{
				var result = await _authenticationService.RegisterAsync(model);

				if(result.StatusCode == 200)
				{
					return Ok(result);
				}
				else if(result.StatusCode == 404)
				{
					return NotFound(result);
				}
				else
				{
					return BadRequest(result);
				}
			}
			return BadRequest(model);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginModel)
		{
			var response = await _authenticationService.Login(loginModel);

			if(response.StatusCode == "Success")
			{
				return Ok(response);
			}
			else if(response.StatusCode == "Error")
			{
				return Unauthorized(response);
			}

			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			if(string.IsNullOrEmpty(email))
				return BadRequest("Email is required");

			var result = await _authenticationService.ForgotPasswordAsync(email);

			if(result.IsSuccess)
				return Ok(result); // 200

			if(result.StatusCode == "404")
				return NotFound(result); // 404

			return BadRequest(result); // 400
		}

		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
		{
			if(ModelState.IsValid)
			{
				var result = await _authenticationService.ResetPasswordAsync(model);

				if(result.IsSuccess)
					return Ok(result);

				return BadRequest(result);
			}

			return BadRequest("Some properties are not valid");
		}


		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _authenticationService.ChangePasswordAsync(model.Email, model.CurrentPassword, model.NewPassword);
			if(!result.IsSuccess)
			{
				return BadRequest(result);
			}

			return Ok(result);
		}


	}
}
