﻿using Savi.Core.Interfaces;
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
                var newGroup = await _groupSavingsRepository.GetGroupByIdAsync(GroupId);

                if (newMember != null && newGroup.Result != null)
                {

                    var newGroupMembertoAdd = new GroupSavingsMembers();
                    var newposition = await _groupSavingsMembersRepository.GetUserLastUserPosition();
                    var listOfmembers = await _groupSavingsMembersRepository.GetListOfGroupMembersAsync(GroupId);

                    if (newposition <= newGroup.Result.MemberCount & listOfmembers < newGroup.Result.MemberCount)
                    {
                        newGroupMembertoAdd.UserId = UserId;
                        newGroupMembertoAdd.Positions = ++newposition;
                        newGroupMembertoAdd.IsGroupOwner = IsGroupOwner.No;
                        newGroupMembertoAdd.GroupSavingsId = GroupId;

                        var addNewGroupMember = _groupSavingsMembersRepository.CreateSavingsGroupMembersAsync(newGroupMembertoAdd);
                        if (addNewGroupMember != null)
                        {
                            response.StatusCode = 200;
                            response.DisplayMessage = ($"Successfully added to {newGroup.Result.SavesName} group");
                            response.Result = true;
                            return response;
                        }
                        response.StatusCode = 400;
                        response.DisplayMessage = ($"Unable to add you to {newGroup.Result.SavesName} group");
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
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.DisplayMessage = ("Internal error");
                response.Result = false;
                return response;
            }
        }
    }
}