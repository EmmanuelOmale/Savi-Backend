using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepository;

namespace Savi.Data.Repository
{
    public class SavingGoalRepository : ISavingGoalRepository
    {
        private readonly SaviDbContext _dbContext;

        public SavingGoalRepository(SaviDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseDto<SavingGoal>> CreateGoal(SavingGoal goal)
        {
            var response = new ResponseDto<SavingGoal>();
            try
            {
                
                _dbContext.SavingGoals.Add(goal);
                await _dbContext.SaveChangesAsync();

                response.StatusCode = 200;
                response.DisplayMessage = "Saving goal successfully created";
                response.Result = goal;
            }
            catch (DbUpdateException ex)
            {
               
                response.StatusCode = 400;
                response.DisplayMessage = "An error occurred while creating the saving goal.";
               
            }

            return response;
        }

        public async Task<ResponseDto<SavingGoal>> DeleteGoal(int id)
        {
            var response = new ResponseDto<SavingGoal>();
            var checksaving = await _dbContext.SavingGoals.FindAsync(id);
            if (checksaving == null)
            {
                response.DisplayMessage = "Error";
                response.StatusCode = StatusCodes.Status404NotFound;
                return response;
            }

            var delete = _dbContext.SavingGoals.Remove(checksaving);
            var changes = await _dbContext.SaveChangesAsync();
            if (changes > 0)
            {
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                return response;
            }
            response.DisplayMessage = "Error";
            response.StatusCode = StatusCodes.Status501NotImplemented;
            return response;

        }

        public async Task<ResponseDto<List<SavingGoal>>> GetAllGoals()
        {
            var response = new ResponseDto<List<SavingGoal>>();
            try
            {
             
                var goals = await _dbContext.SavingGoals.ToListAsync();

                response.StatusCode = 200;
                response.DisplayMessage = "All Saving goals retrieved.";
                response.Result = goals;
            }
            catch (DbUpdateException ex)
            {
               
                response.StatusCode = 400;
                response.DisplayMessage = "An error occurred while retrieving the saving goals.";
               
            }

            return response;
        }

        public async Task<ResponseDto<SavingGoal>> GetGoalById(int id)
        {
            var response = new ResponseDto<SavingGoal>();
            try
            {
                var goal = await _dbContext.SavingGoals.FindAsync(id);
                if (goal != null)
                {
                    response.StatusCode = 200;
                    response.DisplayMessage = "Saving goal found";
                    response.Result = goal;
                }
                else
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Saving goal not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.DisplayMessage = "An error occurred while retrieving the saving goal.";
            }

            return response;
        }

        public async Task<ResponseDto<SavingGoal>> UpdateGoal(int goalId, SavingGoal updatedGoal)
        {
            var response = new ResponseDto<SavingGoal>();
            try
            {
                var existingGoal = await _dbContext.SavingGoals.FindAsync(goalId);
                if (existingGoal != null)
                {
                    _dbContext.Entry(existingGoal).CurrentValues.SetValues(updatedGoal);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = 200;
                    response.DisplayMessage = "Saving goal successfully updated";
                    response.Result = updatedGoal;
                }
                else
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Saving goal not found";
                }
            }
            catch (DbUpdateException ex)
            {
                response.StatusCode = 400;
                response.DisplayMessage = "An error occurred while updating the saving goal.";
            }

            return response;
        }
    }
}
