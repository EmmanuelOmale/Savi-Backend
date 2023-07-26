using Microsoft.AspNetCore.Mvc;
using Savi.Api.Service;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
    [ApiController]
    [Route("api/SavingGoals")]
    public class SavingGoalController : ControllerBase
    {
        private readonly ISavingGoalService _goalService;
        private readonly ISavingsService _savingsService;

		public ISavingGoalService Object { get; }

		public SavingGoalController(ISavingGoalService goalService, ISavingsService savings)
        {
            _goalService = goalService;
            _savingsService = savings;
        }

		

		[HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto<SavingGoal>>> CreateGoal(SavingGoal goal)
        {
            try
            {
                var response = await _goalService.CreateGoal(goal);

                if (response.StatusCode == 200)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto<List<SavingGoal>>>> GetAllGoals()
        {
            try
            {
                var response = await _goalService.GetAllGoals();

                if (response.StatusCode == 200)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto<SavingGoal>>> GetGoalById(int id)
        {
            var response = await _goalService.GetGoalById(id);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpDelete("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto<SavingGoal>>> DeleteGoal(int id)
        {
            var result = await _goalService.DeleteGoal(id);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpPut("{id}/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto<SavingGoal>>> UpdateGoal(int id, SavingGoal updatedGoal)
        {
            var result = await _goalService.UpdateGoal(id, updatedGoal);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("{id}/fundtarget")]
        public async Task<ActionResult<APIResponse>> FundTarget(int id, decimal amount)
		{
			
			var saving = await _savingsService.FundTargetSavings(id,amount);
			if(saving == null)
            {
                return NotFound();
            }
            return Ok(saving);
		}

    }
}
