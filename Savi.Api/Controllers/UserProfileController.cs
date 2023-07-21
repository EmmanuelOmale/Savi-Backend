using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("id")]
        public async Task<APIResponse> Get(string id)
        {
           var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                var response = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "user not found",
                };
                return response;
            }
            else
            {
                var response = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = true,
                    Message = "user found",
                    Result = user
                    
                };
                return response;
            }
        }
    }
}
