using Microsoft.AspNetCore.Identity;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Core.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

    }
}
