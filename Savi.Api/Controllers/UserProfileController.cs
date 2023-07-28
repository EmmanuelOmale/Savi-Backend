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
                    StatusCode = StatusCodes.Status200OK.ToString(),
                    IsSuccess = true,
                    Message = "user found",
                    Result = user
                    
                };
                return response;
            }
        }

        [HttpGet("token")]
        public async Task<IActionResult>GetUserByToken(string token)
        {
            var user = await _unitOfWork.UserRepository.GetLoggedInUserByToken(token);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string userId, UserDTO userDTO)
        {
            var user = _userRepository.UpdateUser(userId, userDTO);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            return Ok(user);
        }
    }
}
