using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<ResponseDto<UserDTO>> GetUserByIdAsync(string Id);
        public ApplicationUser FinduserByPhoneNumber(string Phonenumber);
    }
}
