using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Core.PersonalSaving
{
    public interface IPersonalSavings
    {
        Task<ResponseDto<string>> SetPersonal_Savings_Target(SetTarget setTarget);
        Task<ResponseDto<string>> UpPersonal_Savings_Target(UpdatSetTargetDTO setTarget);
        Task<ResponseDto<string>> Delete_Personal_TargetSavings(string Id);
        Task<ResponseDto<IEnumerable<SetTarget>>> Get_ListOf_UserTargets(string UserId);
        Task<ResponseDto<SetTarget>> GetTargetById(string Id);
        Task<ResponseDto<IEnumerable<SetTarget>>> Get_ListOfAll_Targets();
    }
}
