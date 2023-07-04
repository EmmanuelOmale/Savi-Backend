using Microsoft.AspNetCore.Identity;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;

namespace Savi.Core.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
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

    }
}
