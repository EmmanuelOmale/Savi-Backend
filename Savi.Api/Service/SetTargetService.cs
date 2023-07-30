using Savi.Api.Service;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;
using Savi.Data.IRepository;
using Savi.Data.Repositories;

public class SetTargetService : ISetTargetService
{
    private readonly ISetTargetRepository _targetRepository;

    public SetTargetService(ISetTargetRepository targetRepository)
    {
        _targetRepository = targetRepository;
    }
   

    public async Task<ResponseDto<SetTarget>> DeleteTarget(Guid id)
    {
        return await _targetRepository.DeleteTarget(id);
    }

    public async Task<ResponseDto<IEnumerable<SetTarget>>> GetAllTargets()
    {
        return await _targetRepository.GetAllTargets();
    }

    public async Task<ResponseDto<SetTarget>> GetTargetById(Guid id)
    {
        return await _targetRepository.GetTargetById(id);
    }

    public async Task<ResponseDto<SetTarget>> CreateTarget(SetTarget setTarget, string userId)
    {
      return await _targetRepository.CreateTarget(setTarget, userId);
    }


    public async Task<ResponseDto<SetTarget>> UpdateTarget(Guid id, SetTarget SetTarget)
    {
        return await _targetRepository.UpdateTarget(id, SetTarget);
    }
	public async Task<List<SetTarget>> GetSetTargetsByUserId(string userId)
	{
		return await _targetRepository.GetSetTargetsByUserId(userId);
	}
}