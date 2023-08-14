using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;


        public SavingGoalRepository(SaviDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto<SavingGoalsDTO>> CreateGoal(SavingGoal goal)
        {
            var goals = _mapper.Map<SavingGoalsDTO>(goal);

            var response = new ResponseDto<SavingGoalsDTO>();
            try
            {

                _dbContext.SavingGoals.Add(goal);
                await _dbContext.SaveChangesAsync();

                response.StatusCode = 200;
                response.DisplayMessage = "Saving goal successfully created";
                response.Result = goals;
            }
            catch(DbUpdateException ex)
            {

                response.StatusCode = 400;
                response.DisplayMessage = (ex.Message);

            }

            return response;
        }

        public async Task<ResponseDto<SavingGoalsDTO>> DeleteGoal(int id)
        {
            var response = new ResponseDto<SavingGoalsDTO>();
            var checksaving = await _dbContext.SavingGoals.FindAsync(id);
            if(checksaving == null)
            {
                response.DisplayMessage = "Error";
                response.StatusCode = StatusCodes.Status404NotFound;
                return response;
            }

            var delete = _dbContext.SavingGoals.Remove(checksaving);
            var changes = await _dbContext.SaveChangesAsync();
            if(changes > 0)
            {
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                return response;
            }
            response.DisplayMessage = "Error";
            response.StatusCode = StatusCodes.Status501NotImplemented;
            return response;

        }

        public async Task<ResponseDto<List<SavingGoalsDTO>>> GetAllGoals()
        {
            var response = new ResponseDto<List<SavingGoalsDTO>>();
            try
            {

                var goals = await _dbContext.SavingGoals.ToListAsync();
                var goal = _mapper.Map<List<SavingGoalsDTO>>(goals);

                response.StatusCode = 200;
                response.DisplayMessage = "All Saving goals retrieved.";
                response.Result = goal;
            }
            catch(DbUpdateException ex)
            {

                response.StatusCode = 400;
                response.DisplayMessage = (ex.Message);

            }

            return response;
        }

        public async Task<ResponseDto<SavingGoalsDTO>> GetGoalById(int id)
        {
            var response = new ResponseDto<SavingGoalsDTO>();
            try
            {
                var goal = await _dbContext.SavingGoals.FindAsync(id);
                var goals = _mapper.Map<SavingGoalsDTO>(goal);
                if(goal != null)
                {
                    response.StatusCode = 200;
                    response.DisplayMessage = "Saving goal found";
                    response.Result = goals;
                }
                else
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Saving goal not found";
                }
            }
            catch(Exception ex)
            {
                response.StatusCode = 400;
                response.DisplayMessage = (ex.Message);
            }

            return response;
        }
        public async Task<ResponseDto<SavingGoalsDTO>> UpdateGoal(int goalId, SavingGoal updatedGoal)
        {
            var response = new ResponseDto<SavingGoalsDTO>();
            try
            {
                var existingGoal = await _dbContext.SavingGoals.FindAsync(goalId);
                if(existingGoal != null)
                {
                    var goals = _mapper.Map<SavingGoalsDTO>(updatedGoal);
                    _dbContext.Entry(existingGoal).CurrentValues.SetValues(goals);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = 200;
                    response.DisplayMessage = "Saving goal successfully updated";
                    response.Result = goals;
                }
                else
                {
                    response.StatusCode = 404;
                    response.DisplayMessage = "Saving goal not found";
                }
            }
            catch(DbUpdateException ex)
            {
                response.StatusCode = 400;
                response.DisplayMessage = (ex.Message);
            }

            return response;
        }
    }
}