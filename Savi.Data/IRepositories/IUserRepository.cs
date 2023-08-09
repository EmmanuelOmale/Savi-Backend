using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
	public interface IUserRepository
	{
		Task<ResponseDto<UserDTO>> GetUserByIdAsync(string Id);

		public ApplicationUser FinduserByPhoneNumber(string Phonenumber);

		Task<ApplicationUser> GetLoggedInUserByToken(string token);

		Task<ResponseDto<UserDTO>> UpdateUser(string userId, UserDTO updateUserDto);

		public Task<ApplicationUser> GetUserById(string Id);
	}
}