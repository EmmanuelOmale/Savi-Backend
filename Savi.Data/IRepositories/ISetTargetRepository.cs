using Savi.Data.Domains;

namespace Savi.Data.IRepositories
{
    public interface ISetTargetRepository
    {
        Task<bool> CreateTarget(SetTarget setTarget);

        Task<IEnumerable<SetTarget>> GetAllTargets();

        Task<SetTarget> GetTargetById(string TargetId);

        Task<bool> UpdateTarget(SetTarget SetTarget);

        Task<bool> DeleteTarget(string Id);

        Task<List<SetTarget>> GetSetTargetsByUserId(string userId);
    }
}