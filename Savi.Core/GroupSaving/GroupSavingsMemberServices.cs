using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;
using Savi.Data.IRepositories;

namespace Savi.Core.GroupSaving
{
    public class GroupSavingsMemberServices : IGroupSavingsMemberServices
    {
        private readonly IGroupSavingsMembersRepository _groupSavingsMembersRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupSavingsRepository _groupSavingsRepository;

        public GroupSavingsMemberServices(IGroupSavingsMembersRepository groupSavingsMembersRepository,
            IUserRepository userRepository, IGroupSavingsRepository groupSavingsRepository)
        {
            _groupSavingsMembersRepository = groupSavingsMembersRepository;
            _userRepository = userRepository;
            _groupSavingsRepository = groupSavingsRepository;
        }

        public async Task<ResponseDto<bool>> JoinGroupSavings(string UserId, string GroupId)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var newMember = await _userRepository.GetUserByIdAsync(UserId);
                var checkmember = _groupSavingsMembersRepository.Check_If_UserExist(UserId);
                if(checkmember)
                {
                    response.DisplayMessage = $"User with the walletId" +
                        $" {newMember.Result.WalletId} has already joined the group";
                    response.Result = false;
                    response.StatusCode = 401;
                    return response;
                }
                var newGroup = await _groupSavingsRepository.GetGroupById(GroupId);

                if(newMember != null && newGroup != null)
                {

                    var newGroupMembertoAdd = new GroupSavingsMembers();
                    var newposition = await _groupSavingsMembersRepository.GetUserLastUserPosition();
                    if(newposition == 4)
                    {
                        newGroup.ActualStartDate = DateTime.Now;
                        newGroup.NextRunTime = DateTime.Today;
                        newGroup.GroupStatus = GroupStatus.Running;

                        var frequency = newGroup.FrequencyId;
                        if(frequency == 1)
                        {
                            newGroup.ActualEndDate = DateTime.Now.AddDays(6);
                            await _groupSavingsRepository.UpDateGroupSavings(newGroup);

                        }
                        else if(frequency == 2)
                        {
                            newGroup.ActualEndDate = DateTime.Now.AddDays(34);
                            await _groupSavingsRepository.UpDateGroupSavings(newGroup);


                        }
                        else if(frequency == 3)
                        {
                            newGroup.ActualEndDate = DateTime.Now.AddDays(174);
                            await _groupSavingsRepository.UpDateGroupSavings(newGroup);
                        }


                    }

                    var listOfmembers = await _groupSavingsMembersRepository.GetListOfGroupMembersAsync(GroupId);

                    if(newposition <= newGroup.MemberCount & listOfmembers < newGroup.MemberCount)
                    {
                        newGroupMembertoAdd.UserId = UserId;
                        newGroupMembertoAdd.Positions = ++newposition;
                        newGroupMembertoAdd.IsGroupOwner = IsGroupOwner.No;
                        newGroupMembertoAdd.GroupSavingsId = GroupId;

                        var addNewGroupMember = await _groupSavingsMembersRepository.CreateSavingsGroupMembersAsync(newGroupMembertoAdd);
                        if(addNewGroupMember)
                        {
                            response.StatusCode = 200;
                            response.DisplayMessage = ($"Successfully added to {newGroup.SavesName} group");
                            response.Result = true;
                            return response;
                        }
                        response.StatusCode = 400;
                        response.DisplayMessage = ($"Unable to add you to {newGroup.SavesName} group");
                        response.Result = false;
                        return response;
                    }
                    response.StatusCode = 401;
                    response.DisplayMessage = ("Group list is already full, find another group");
                    response.Result = false;
                    return response;
                }
                response.StatusCode = 402;
                response.DisplayMessage = ("group or user doesn't exist");
                response.Result = false;
                return response;

            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.DisplayMessage = (ex.Message);
                response.Result = false;
                return response;
            }


        }
        public async Task<List<GroupMembersDto>> GetListOFGroupMember(string GroupId)
        {
            try
            {
                var members = await _groupSavingsMembersRepository.GetListOfGroupMembersAsync3(GroupId);
                if(members.Count > 0)
                {
                    return members;
                }
                return null;
            }
            catch(Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
