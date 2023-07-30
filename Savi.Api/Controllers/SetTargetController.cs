using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savi.Api.Service;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetTargetController : ControllerBase
    {
        private readonly ISetTargetService _setTargetService;
        private readonly IMapper _mapper;

        public SetTargetController(ISetTargetService setTargetService, IMapper mapper)
        {
            _setTargetService = setTargetService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<SetTarget>>>> GetAllTargets()
        {
            var response = await _setTargetService.GetAllTargets();
           StatusCode(response.StatusCode);
            return Ok(response);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<SetTarget>>> GetTargetById(Guid id)
        {
            try
            {
                var response = await _setTargetService.GetTargetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Exception occurred while fetching target by ID: {ex}");

               
                var errorResponse = new ResponseDto<SetTarget>
                {
                    StatusCode = 500, 
                    DisplayMessage = "An error occurred while fetching the target. Please try again later.",
                    Result = null,
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }



        [HttpPost]
        public async Task<ActionResult<ResponseDto<SetTarget>>> CreateTarget([FromBody] SetTarget setTarget)
        {
            try
            {
                var response = await _setTargetService.CreateTarget(setTarget);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Exception occurred during target creation: {ex}");

                
                var errorResponse = new ResponseDto<SetTarget>
                {
                    StatusCode = 500, 
                    DisplayMessage = "An error occurred during target creation. Please try again later.",
                    Result = null,
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }


        [HttpPut("{id}")]
            public async Task<IActionResult> UpdateTarget(Guid id, [FromBody] SetTarget updatedTarget)
        {
           
            if (id != updatedTarget.Id)
            {
                return BadRequest("The provided ID does not match the target ID in the request body.");
            }

            
            if (updatedTarget.Frequency > FrequencyType.Daily)
            {
                return BadRequest("Frequency cannot be more than Daily");
            }

            var response = await _setTargetService.UpdateTarget(id, updatedTarget);

            if (response.StatusCode == 200)
            {
                return Ok(response.Result);
            }
            else if (response.StatusCode == 404)
            {
                return NotFound(response.DisplayMessage);
            }
            else
            {
                return StatusCode(response.StatusCode, response.DisplayMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<SetTarget>>> DeleteTarget(Guid id)
        {
            try
            {
                var response = await _setTargetService.DeleteTarget(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Exception occurred while deleting target: {ex}");

                var errorResponse = new ResponseDto<SetTarget>
                {
                    StatusCode = 500,
                    DisplayMessage = "An error occurred while deleting the target. Please try again later.",
                    Result = null,
                };

                return StatusCode(errorResponse.StatusCode, errorResponse);
            }
        }

    }


}
