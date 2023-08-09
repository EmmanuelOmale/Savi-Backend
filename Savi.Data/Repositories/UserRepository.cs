using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Savi.Data.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        private readonly SaviDbContext _saviDbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserRepository(SaviDbContext db, IMapper mapper, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IServiceScopeFactory serviceScopeFactory) : base(db)
        {
            _saviDbContext = db;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<ResponseDto<UserDTO>> GetUserByIdAsync(string Id)
        {
            var user = await _saviDbContext.Users.FindAsync(Id);
            if (user == null)
            {
                var notFoundResponse = new ResponseDto<UserDTO>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    DisplayMessage = "User not found"
                };
                return notFoundResponse;
            }
            var result = _mapper.Map<UserDTO>(user);

            var success = new ResponseDto<UserDTO>
            {
                StatusCode = StatusCodes.Status200OK,
                DisplayMessage = "User Found",
                Result = result
            };
            return success;

        }

        public async Task<ApplicationUser> GetUserById(string Id)
        {
            try
            {
                var scope = _serviceScopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SaviDbContext>();
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);

                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<ApplicationUser> FinduserByPhoneNumber(string Phonenumber)
        {
            var UserPhoneNumber = await _saviDbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == Phonenumber);
            if (UserPhoneNumber == null)
            {
                return null;
            }
            return UserPhoneNumber;
        }

        public async Task<ApplicationUser> GetLoggedInUserByToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = authSigningKey,
                    ValidateIssuerSigningKey = true
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var claims = principal.Claims;

                var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (userName == null)
                {
                    throw new Exception("Invalid token. Username not found in token claims.");
                }

                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    throw new Exception("User not found for the provided token.");
                }

                return user;
            }
            catch (SecurityTokenValidationException ex)
            {
                throw new Exception("Invalid token. " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting the user from the token. " + ex.Message);
            }
        }


        public async Task<ResponseDto<UserDTO>> UpdateUser(string userId, UserDTO updateUserDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new Exception("User not found for the provided user ID.");
                }

                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.PhoneNumber = updateUserDto.PhoneNumber;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    throw new Exception("Error while updating user information.");
                }

                return new ResponseDto<UserDTO>
                {
                    StatusCode = 200,
                    DisplayMessage = "User information updated successfully.",
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating user information. " + ex.Message);
            }
        }
    }
}
