using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Core.PersonalSaving
{
    public class SetTargetFundingService : ISetTargetFundingService
    {
        private readonly ITargetFundingRepo _targetFunding;
        public SetTargetFundingService(ITargetFundingRepo targetFunding)
        {
            _targetFunding = targetFunding;
        }
        public async Task<bool> CreateTargetFund(SetTargetFunding setTarget)
        {
            try
            {
                var targetfund = await _targetFunding.CreateTargetFundingAsync(setTarget);
                if(targetfund)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResponseDto<IEnumerable<SetTargetFundingDTO>>> Get_ListOfAll_TargeFunds(string UserId)
        {
            var response = new ResponseDto<IEnumerable<SetTargetFundingDTO>>();
            try
            {
                var listOfTargetsfunds = await _targetFunding.GetListOfTargetFundingAsync(UserId);
                if(listOfTargetsfunds.Any())
                {
                    response.DisplayMessage = "Success";
                    response.Result = listOfTargetsfunds;
                    response.StatusCode = 200;
                    return response;
                }
                response.DisplayMessage = "Failed";
                response.Result = null;
                response.StatusCode = 400;
                return response;
            }
            catch(Exception ex)
            {
                response.DisplayMessage = ex.Message;
                response.Result = null;
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
