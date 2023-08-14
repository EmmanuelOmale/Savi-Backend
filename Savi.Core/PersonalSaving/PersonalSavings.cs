using AutoMapper;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;
using Savi.Data.IRepositories;

namespace Savi.Core.PersonalSaving
{
    public class PersonalSavings : IPersonalSavings
    {
        private readonly ISetTargetRepository _setTarget;
        private readonly IMapper _mapper;
        public PersonalSavings(ISetTargetRepository setTarget, IMapper mapper)
        {
            _setTarget = setTarget;
            _mapper = mapper;
        }
        public async Task<ResponseDto<string>> SetPersonal_Savings_Target(SetTarget setTarget)
        {
            var response = new ResponseDto<string>();
            try
            {
                if(setTarget.Frequency == FrequencyType.Daily)
                {
                    setTarget.NextRuntime = DateTime.Today;
                    var x = setTarget.TargetAmount;
                    var y = setTarget.AmountToSave;
                    var z = x / y;
                    setTarget.EndDate = DateTime.Now.AddDays(((double)z));
                    setTarget.WithdrawalDate = setTarget.EndDate.AddDays(1);
                    var newTarget = await _setTarget.CreateTarget(setTarget);
                    if(newTarget)
                    {
                        response.DisplayMessage = "Success";
                        response.Result = $"Your target of amount {setTarget.TargetAmount} has been successfully created";
                        response.StatusCode = 200;
                        return response;
                    }
                    response.DisplayMessage = "Failed";
                    response.Result = $"Unable to create target of amount{setTarget.TargetAmount}";
                    response.StatusCode = 400;
                    return response;
                }
                else if(setTarget.Frequency == FrequencyType.Weekly)
                {
                    setTarget.NextRuntime = DateTime.Today;
                    var x = setTarget.TargetAmount;
                    var y = setTarget.AmountToSave;
                    var z = (x / y) * 6;
                    setTarget.EndDate = DateTime.Now.AddDays(((double)z));
                    setTarget.WithdrawalDate = setTarget.EndDate.AddDays(1);
                    var newTarget = await _setTarget.CreateTarget(setTarget);
                    if(newTarget)
                    {
                        response.DisplayMessage = "Success";
                        response.Result = $"Your target of amount {setTarget.TargetAmount} has been successfully created";
                        response.StatusCode = 200;
                        return response;
                    }
                    response.DisplayMessage = "Failed";
                    response.Result = $"Unable to create target of amount{setTarget.TargetAmount}";
                    response.StatusCode = 400;
                    return response;
                }
                else
                {
                    setTarget.NextRuntime = DateTime.Today;
                    var x = setTarget.TargetAmount;
                    var y = setTarget.AmountToSave;
                    var z = (x / y) * 30;
                    setTarget.EndDate = DateTime.Now.AddDays(((double)z));
                    setTarget.WithdrawalDate = setTarget.EndDate.AddDays(1);
                    var newTarget = await _setTarget.CreateTarget(setTarget);
                    if(newTarget)
                    {
                        response.DisplayMessage = "Success";
                        response.Result = $"Your target of amount {setTarget.TargetAmount} has been successfully created";
                        response.StatusCode = 200;
                        return response;
                    }
                    response.DisplayMessage = "Failed";
                    response.Result = $"Unable to create target of amount{setTarget.TargetAmount}";
                    response.StatusCode = 400;
                    return response;
                }
            }
            catch(Exception ex)
            {

                response.DisplayMessage = ex.Message;
                response.Result = $"Unable to create target of amount{setTarget.TargetAmount}";
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> Delete_Personal_TargetSavings(string Id)
        {
            var response = new ResponseDto<string>();
            try
            {
                var targetToDelete = await _setTarget.DeleteTarget(Id);
                if(targetToDelete)
                {
                    response.DisplayMessage = "Success";
                    response.Result = "Target Deleted Successfully";
                    response.StatusCode = 200;
                    return response;
                }
                response.DisplayMessage = "Failed";
                response.Result = "Failed to delete target";
                response.StatusCode = 400;
                return response;

            }
            catch(Exception ex)
            {
                response.DisplayMessage = ex.Message;
                response.Result = "error trying to delete target";
                response.StatusCode = 500;
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<SetTarget>>> Get_ListOf_UserTargets(string UserId)
        {
            var response = new ResponseDto<IEnumerable<SetTarget>>();
            try
            {
                var listOfTargets = await _setTarget.GetSetTargetsByUserId(UserId);
                if(listOfTargets.Any())
                {
                    response.DisplayMessage = "Success";
                    response.Result = listOfTargets;
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
        public async Task<ResponseDto<IEnumerable<SetTarget>>> Get_ListOfAll_Targets()
        {
            var response = new ResponseDto<IEnumerable<SetTarget>>();
            try
            {
                var listOfTargets = await _setTarget.GetAllTargets();
                if(listOfTargets.Any())
                {
                    response.DisplayMessage = "Success";
                    response.Result = listOfTargets;
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
        public async Task<ResponseDto<SetTarget>> GetTargetById(string Id)
        {
            var response = new ResponseDto<SetTarget>();
            try
            {
                var Target = await _setTarget.GetTargetById(Id);
                if(Target != null)
                {
                    response.DisplayMessage = "Success";
                    response.Result = Target;
                    response.StatusCode = 200;
                    return response;
                }
                response.DisplayMessage = "Invalid Target Id";
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
        public async Task<ResponseDto<string>> UpPersonal_Savings_Target(UpdatSetTargetDTO setTarget)
        {
            var response = new ResponseDto<string>();
            try
            {
                if(setTarget != null)
                {
                    var targetMap = _mapper.Map<SetTarget>(setTarget);
                    var newTarget = await _setTarget.UpdateTarget(targetMap);
                    if(newTarget)
                    {
                        response.DisplayMessage = "Success";
                        response.Result = $"Your target of amount {setTarget.TargetAmount} has updated";
                        response.StatusCode = 200;
                        return response;
                    }
                    response.DisplayMessage = "Failed";
                    response.Result = $"Unable to create target of amount{setTarget.TargetAmount}";
                    response.StatusCode = 401;
                    return response;
                }
                response.DisplayMessage = "Failed";
                response.Result = $"Unable to create target of amount{setTarget.TargetAmount} Check TargetId";
                response.StatusCode = 400;
                return response;
            }
            catch(Exception ex)
            {
                response.DisplayMessage = ex.Message;
                response.Result = $"Unable to create target of amount{setTarget.TargetAmount}";
                response.StatusCode = 400;
                return response;
            }
        }
    }
}
