using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Savi.Core.PersonalSaving;
using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetTargetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonalSavings _personalSavings;
        private readonly IAutoTargetFund _autoTarget;
        public SetTargetController(IMapper mapper,
           IPersonalSavings personalSavings, IAutoTargetFund autoTarget)
        {
            _mapper = mapper;
            _personalSavings = personalSavings;
            _autoTarget = autoTarget;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateTarget([FromBody] SetTargetDTO setTarget)
        {
            var target = _mapper.Map<SetTarget>(setTarget);
            var response = await _personalSavings.SetPersonal_Savings_Target(target);
            if(response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTarget([FromBody] UpdatSetTargetDTO updatedTarget)
        {

            var response = await _personalSavings.UpPersonal_Savings_Target(updatedTarget);

            if(response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpDelete("delete/{TargetId}")]
        public async Task<IActionResult> DeleteTarget(string TargetId)
        {
            var response = await _personalSavings.Delete_Personal_TargetSavings(TargetId);
            if(response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("list/{UserId}")]
        public async Task<IActionResult> GetAllTargets(string UserId)
        {
            var response = await _personalSavings.Get_ListOf_UserTargets(UserId);
            if(response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("list-all-targets")]
        public async Task<IActionResult> GetlistofAllTargets()
        {
            var response = await _personalSavings.Get_ListOfAll_Targets();
            if(response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
        [HttpGet("{TargetId}")]
        public async Task<IActionResult> GetTargetById(string TargetId)
        {
            var response = await _personalSavings.GetTargetById(TargetId);
            if(response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
        [HttpPost("autotarget")]
        public async Task<IActionResult> Autofunding()
        {
            var response = await _autoTarget.AutoTarget();
            if(response)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
    }
}