using Microsoft.AspNetCore.Identity;
using Savi.Data.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Savi.Core.Interfaces
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync(SignUpDto signUpDto);
        Task<APIResponse> Login(LoginRequestDTO loginModel);
        JwtSecurityToken GetToken(List<Claim> authClaims);


    }
}
