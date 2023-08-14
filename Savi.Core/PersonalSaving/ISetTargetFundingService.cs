using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Core.PersonalSaving
{
    public interface ISetTargetFundingService
    {
        Task<bool> CreateTargetFund(SetTargetFunding setTarget);
        Task<ResponseDto<IEnumerable<SetTargetFundingDTO>>> Get_ListOfAll_TargeFunds(string UserId);
    }
}
