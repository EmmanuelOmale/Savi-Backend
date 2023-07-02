using Microsoft.AspNetCore.Identity;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> RegisterAsync(SignUpDto signUpDto);

    }
}
