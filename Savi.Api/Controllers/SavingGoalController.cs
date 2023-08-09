﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Savi.Api.Service;
using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
	[ApiController]
	[Route("api/SavingGoals")]
	public class SavingGoalController : ControllerBase
	{
		private readonly ISavingGoalService _goalService;
		private readonly IMapper _mapper;

		public ISavingGoalService Object { get; }

		public SavingGoalController(ISavingGoalService goalService, IMapper mapper)
		{
			_goalService = goalService;
			_mapper = mapper;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseDto<SavingGoalsDTO>>> CreateGoal(SavingGoalsDTO goal)
		{
			try
			{
				var goals = _mapper.Map<SavingGoal>(goal);
				var response = await _goalService.CreateGoal(goals);

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
		public async Task<ActionResult<ResponseDto<List<SavingGoalsDTO>>>> GetAllGoals()
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
		public async Task<ActionResult<ResponseDto<SavingGoalsDTO>>> GetGoalById(int id)
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
		public async Task<ActionResult<ResponseDto<SavingGoalsDTO>>> DeleteGoal(int id)
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
		public async Task<ActionResult<ResponseDto<SavingGoalsDTO>>> UpdateGoal(int id, SavingGoal updatedGoal)
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
	}
}