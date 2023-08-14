using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
    public interface ITargetFundingRepo
    {
        Task<bool> CreateTargetFundingAsync(SetTargetFunding funding);
        Task<IEnumerable<SetTargetFundingDTO>> GetListOfTargetFundingAsync(string UserId);


    }
}
