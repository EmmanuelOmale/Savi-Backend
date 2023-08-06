using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<ResponseDto<UserDTO>> GetUserByIdAsync(string Id);
        Task<ApplicationUser> FinduserByPhoneNumber(string Phonenumber);
        Task<ApplicationUser> GetLoggedInUserByToken(string token);
        Task<ResponseDto<UserDTO>> UpdateUser(string userId, UserDTO updateUserDto);
        Task<ApplicationUser> GetUserById(string Id);



    }
}
