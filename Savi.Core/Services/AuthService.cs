using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Savi.Core.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(SignUpDto signUpDto)
        {
            var findEmail = await _userManager.FindByEmailAsync(signUpDto.UserName);
            if (findEmail != null)
            {
                throw new Exception("UserName already exist");
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.UserName,
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,

            };
            var userAction = UserAction.Registration;
            await _emailService.SendMail(userAction, user.Email);
            var regUser = await _userManager.CreateAsync(user, signUpDto.Password);
            if (regUser.Succeeded)
            {
                
                return regUser;
            }
            else
            {
                throw new Exception("Registration not completed");
            }

        }

        public async Task<APIResponse> Login(LoginRequestDTO loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user == null)
            {
                return new APIResponse { StatusCode = "Error", Message = "Invalid username or password." };
            }



            if (await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                var jwtToken = GetToken(authClaims);

                return new APIResponse { StatusCode = "Success", Token = new JwtSecurityTokenHandler().WriteToken(jwtToken), Expiration = jwtToken.ValidTo };
            }

            return new APIResponse { StatusCode = "Error", Message = "Invalid username or password." };
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }


    }
}
