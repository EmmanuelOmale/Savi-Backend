using Microsoft.AspNetCore.Identity;

//using Savi.Api.Models;
using Savi.Data.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Savi.Core.Interfaces
{
	public interface IAuthService
	{
		public Task<ResponseDto<IdentityResult>> RegisterAsync(SignUpDto signUpDto);

		Task<APIResponse> Login(LoginRequestDTO loginModel);

		JwtSecurityToken GetToken(List<Claim> authClaims);

		Task<APIResponse> ForgotPasswordAsync(string email);

		Task<APIResponse> ResetPasswordAsync(ResetPasswordViewModel model);
	}
}