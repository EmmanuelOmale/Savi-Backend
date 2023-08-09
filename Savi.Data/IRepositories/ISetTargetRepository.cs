using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
	public interface ISetTargetRepository
	{
		Task<ResponseDto<SetTarget>> CreateTarget(SetTarget setTarget, string userId);

		Task<ResponseDto<IEnumerable<SetTarget>>> GetAllTargets();

		Task<ResponseDto<SetTarget>> GetTargetById(Guid id);

		Task<ResponseDto<SetTarget>> UpdateTarget(Guid id, SetTarget SetTarget);

		Task<ResponseDto<SetTarget>> DeleteTarget(Guid id);

		Task<List<SetTarget>> GetSetTargetsByUserId(string userId);
	}
}