using Microsoft.AspNetCore.Identity;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
