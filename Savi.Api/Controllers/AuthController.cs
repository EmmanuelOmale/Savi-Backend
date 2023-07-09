﻿using Microsoft.AspNetCore.Mvc;
using Savi.Api.Models;
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

            var result = await _authenticationService.RegisterAsync(model);

            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginModel)
        {
            var response = await _authenticationService.Login(loginModel);

            if (response.StatusCode == "Success")
            {
                return Ok(response);
            }
            else if (response.StatusCode == "Error")
            {
                return Unauthorized(response);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        

        [HttpPost("InitiateResetPassword")]
        public async Task<IActionResult> InitiateResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _authenticationService.InitiateResetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }

        
        [HttpPost("CompleteResetPassword")]
        public async Task<IActionResult> CompletePasswordReset([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.CompletePasswordResetAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }




    }
}
