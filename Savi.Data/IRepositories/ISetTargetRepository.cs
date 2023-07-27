using Savi.Data.Domains;
using Savi.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.IRepositories
{
    public interface ISetTargetRepository
    {
        
        Task<ResponseDto<SetTarget>> CreateTarget(SetTarget setTarget);
        Task<ResponseDto<IEnumerable<SetTarget>>> GetAllTargets();
        Task<ResponseDto<SetTarget>> GetTargetById(Guid id);
        Task<ResponseDto<SetTarget>> UpdateTarget(Guid id, SetTarget SetTarget);
        Task<ResponseDto<SetTarget>> DeleteTarget(Guid id);
    }

}
