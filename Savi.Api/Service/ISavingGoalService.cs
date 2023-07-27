
using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Api.Service
{
    public interface ISavingGoalService
    {
        Task<ResponseDto<SavingGoalsDTO>> CreateGoal(SavingGoal goal);
        Task<ResponseDto<List<SavingGoalsDTO>>> GetAllGoals();
        Task<ResponseDto<SavingGoalsDTO>> GetGoalById(int id);
        Task<ResponseDto<SavingGoalsDTO>> UpdateGoal(int id, SavingGoal updatedGoal);
        Task<ResponseDto<SavingGoalsDTO>> DeleteGoal(int id);
    }
}
