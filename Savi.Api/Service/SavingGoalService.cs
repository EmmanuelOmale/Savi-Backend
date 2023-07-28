using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepository;

namespace Savi.Api.Service
{
    public class SavingGoalService : ISavingGoalService
    {
        private readonly ISavingGoalRepository _goalRepository;

        public SavingGoalService(ISavingGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public async Task<ResponseDto<SavingGoalsDTO>> CreateGoal(SavingGoal goal)
        {
            return await _goalRepository.CreateGoal(goal);
        }

        public async Task<ResponseDto<SavingGoalsDTO>> DeleteGoal(int id)
        {
                return await _goalRepository.DeleteGoal(id); 
            
        }

        public async Task<ResponseDto<List<SavingGoalsDTO>>> GetAllGoals()
        {
            return await _goalRepository.GetAllGoals();
        }

        public async Task<ResponseDto<SavingGoalsDTO>> GetGoalById(int id)
        {
            return await _goalRepository.GetGoalById(id);
        }

        public async Task<ResponseDto<SavingGoalsDTO>> UpdateGoal(int id, SavingGoal updatedGoal)
        {
            return await _goalRepository.UpdateGoal(id, updatedGoal);
        }
    }

}
