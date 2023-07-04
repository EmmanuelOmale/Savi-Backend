using Microsoft.AspNetCore.Identity;
using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync(SignUpDto signUpDto);

    }
}
