using Savi.Api.Models;

namespace Savi.Api.Service
{
    public interface IGoogleSignupService
    {
        Task<UserProfile?> SignupWithGoogleAsync(string code);
    }

}
