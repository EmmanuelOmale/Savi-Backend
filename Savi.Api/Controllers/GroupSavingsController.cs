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
        private readonly IGroupWalletFundingServices _groupWallet;
        public GroupSavingsController(IGroupSavingsServices groupSavingsServices,
            IGroupSavingsMemberServices groupSavingsMemberServices, IGroupWalletFundingServices groupWallet)
        {
            _groupSavingsServices = groupSavingsServices;
            _groupSavingsMemberServices = groupSavingsMemberServices;
            _groupWallet = groupWallet;
        }
        [HttpPost("create/groupsavings")]
        public async Task<IActionResult> CreateGroupsavings(GroupSavingsDto groupSavingsDto)
        {
            var newGroupSavings = await _groupSavingsServices.CreateGroupSavings(groupSavingsDto);
            if(newGroupSavings.Status == true)
            {
                return Ok(newGroupSavings);
            }
            return BadRequest(newGroupSavings);
        }
        [HttpPost("join/groupsavings")]
        public async Task<IActionResult> JoinGroupsavings(string UserId, string GroupId)
        {
            var newGroupSavings = await _groupSavingsMemberServices.JoinGroupSavings(UserId, GroupId);
            if(newGroupSavings.StatusCode == 200)
            {
                return Ok(newGroupSavings);
            }
            return BadRequest(newGroupSavings);
        }
        [HttpGet("get/groupsavings-by/{groupId}")]
        public async Task<IActionResult> GetGroupByIdAsync(string groupId)
        {
            var group = await _groupSavingsServices.GetUserByIDAsync(groupId);
            if(group.StatusCode == 200)
            {
                return Ok(group);
            }
            return BadRequest(group);
        }
        [HttpGet("get/groupsavings-created-by-user/{userId}")]
        public async Task<IActionResult> GetGroupByUserIdAsync(string userId)
        {
            var group = await _groupSavingsServices.GetUserByUserIDAsync(userId);
            if(group.StatusCode == 200)
            {
                return Ok(group);
            }
            return BadRequest(group);
        }
        [HttpGet("get/list/groupsavings")]
        public async Task<IActionResult> GetListOfGroupSavingsAsync()
        {
            var listofgroup = await _groupSavingsServices.GetListOfSavingsGroupAsync();
            if(listofgroup.StatusCode == 200)
            {
                return Ok(listofgroup);
            }
            return BadRequest(listofgroup);
        }
        [HttpGet("get/list-of-all-groupsavings-created-by-user/{UserId}")]
        public async Task<IActionResult> GetListOfGroupSavingsAsync(string UserId)
        {
            var listofgroup = await _groupSavingsServices.GetListOfSavingsGroupByUserIdAsync(UserId);
            if(listofgroup.StatusCode == 200)
            {
                return Ok(listofgroup);
            }
            return BadRequest(listofgroup);
        }
        [HttpPost("auto/fund-groupsavings")]
        public async Task<IActionResult> AutomateSavings()
        {
            var automate = await _groupWallet.GroupAuto();
            if(automate)
            {
                return Ok(automate);
            }
            return BadRequest(automate);
        }
        [HttpGet("list/groupsavingsmembers-in-a-group/{GroupId}")]
        public async Task<IActionResult> GetGroupMembers(string GroupId)
        {
            var members = await _groupSavingsMemberServices.GetListOFGroupMember(GroupId);
            if(members.Count > 0)
            {
                return Ok(members);
            }
            return BadRequest(members);
        }
        [HttpGet("list/groupsavings-a-user-is-in/{UserId}")]
        public async Task<IActionResult> GetGroupMembersByUserId(string UserId)
        {
            var members = await _groupSavingsMemberServices.GetListOFGroupMemberByUserId(UserId);
            if(members.Count > 0)
            {
                return Ok(members);
            }
            return BadRequest(members);
        }
    }
}
