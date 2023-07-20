using Savi.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<ResponseDto<UserDTO>> GetUserByIdAsync(string Id);
    }
}
