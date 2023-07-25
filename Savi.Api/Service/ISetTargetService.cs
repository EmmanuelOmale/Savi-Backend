using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Api.Service
{
    public interface ISetTargetService
    {
        Task<ResponseDto<SetTarget>> CreateTarget(SetTarget setTarget);
        Task<ResponseDto<IEnumerable<SetTarget>>> GetAllTargets();
        Task<ResponseDto<SetTarget>> GetTargetById(Guid id);
        Task<ResponseDto<SetTarget>> UpdateTarget(Guid id, SetTarget SetTarget);
        Task<ResponseDto<SetTarget>> DeleteTarget(Guid id);
    }

}
