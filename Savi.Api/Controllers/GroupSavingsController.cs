using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupSavingsController : ControllerBase

    {
        private readonly IGroupSavingsServices _groupSavingsServices;
        private readonly IGroupSavingsMemberServices _groupSavingsMemberServices;

        public GroupSavingsController(IGroupSavingsServices groupSavingsServices,
            IGroupSavingsMemberServices groupSavingsMemberServices)
        {
            _groupSavingsServices = groupSavingsServices;
            _groupSavingsMemberServices = groupSavingsMemberServices;
        }
        [HttpPost("create/groupsavings")]
        public async Task<IActionResult> CreateGroupsavings(GroupSavingsDto groupSavingsDto)
        {
            var newGroupSavings = await _groupSavingsServices.CreateGroupSavings(groupSavingsDto);
            if (newGroupSavings.Status == true)
            {
                return Ok(newGroupSavings);
            }
            return BadRequest(newGroupSavings);
        }
        [HttpPost("join/groupsavings")]
        public async Task<IActionResult> JoinGroupsavings(string UserId, string GroupId)
        {
            var newGroupSavings = await _groupSavingsMemberServices.JoinGroupSavings(UserId, GroupId);
            if (newGroupSavings.StatusCode == 200)
            {
                return Ok(newGroupSavings);
            }
            return BadRequest(newGroupSavings);
        }
        [HttpGet("get/groupby/{groupId}")]
        public async Task<IActionResult> GetGroupByIdAsync(string groupId)
        {
            var group = await _groupSavingsServices.GetUsrByIDAsync(groupId);
            if (group.StatusCode == 200)
            {
                return Ok(group);
            }
            return BadRequest(group);
        }

        [HttpGet("get/list/groupsavings")]
        public async Task<IActionResult> GetListOfGroupSavingsAsync()
        {
            var listofgroup = await _groupSavingsServices.GetListOfSavingsGroupAsync();
            if (listofgroup.StatusCode == 200)
            {
                return Ok(listofgroup);
            }
            return BadRequest(listofgroup);
        }

    }
}
