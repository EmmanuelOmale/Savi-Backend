using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
    {
        private SaviDbContext _saviDbContext;
        private readonly IMapper _mapper;


        public UserRepository(SaviDbContext db, IMapper mapper) : base(db)
        {
            _saviDbContext = db;
            _mapper = mapper;
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
    }
}
