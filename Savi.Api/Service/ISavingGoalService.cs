
using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Api.Service
{
    public interface ISavingGoalService
    {
        Task<ResponseDto<SavingGoal>> CreateGoal(SavingGoal goal);
        Task<ResponseDto<List<SavingGoal>>> GetAllGoals();
        Task<ResponseDto<SavingGoal>> GetGoalById(int id);
        Task<ResponseDto<SavingGoal>> UpdateGoal(int id, SavingGoal updatedGoal);
        Task<ResponseDto<SavingGoal>> DeleteGoal(int id);
    }
}
