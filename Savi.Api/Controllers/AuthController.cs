using Microsoft.AspNetCore.Mvc;
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

            try
            {
                var result = await _authenticationService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

    }
}
