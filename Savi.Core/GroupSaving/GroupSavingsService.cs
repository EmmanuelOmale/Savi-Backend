using AutoMapper;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Core.GroupSaving
{
    public class GroupSavingsService : IGroupSavingsServices
    {
        private readonly IGroupSavingsRepository _groupSavingsRepository;
        private readonly IMapper _mapper;
        private readonly IGroupSavingsMembersRepository _groupSavingsMembers;
        private readonly IWalletCreditService _walletServices;
        private readonly IUserRepository _userRepository;
        public GroupSavingsService(IGroupSavingsRepository groupSavingsRepository, IMapper mapper, IGroupSavingsMembersRepository groupSavingsMembers,
            IWalletCreditService walletServices, IUserRepository userRepository)
        {
            _groupSavingsRepository = groupSavingsRepository;
            _mapper = mapper;
            _groupSavingsMembers = groupSavingsMembers;
            _walletServices = walletServices;
            _userRepository = userRepository;
        }
        public async Task<PayStackResponseDto> CreateGroupSavings(GroupSavingsDto groupSavingsDto)
        {
            var response = new PayStackResponseDto();
            var newGroupSavings = _mapper.Map<GroupSavings>(groupSavingsDto);
            newGroupSavings.GroupStatus = Data.Enums.GroupStatus.Ongoing;
            var result = await _groupSavingsRepository.CreateGroupSavings(newGroupSavings);
            if(result)
            {
                var newGroupmember = new GroupSavingsMembers();
                newGroupmember.Positions = newGroupmember.Positions;
                await _userRepository.GetUserByIdAsync(groupSavingsDto.UserId);
                newGroupmember.UserId = groupSavingsDto.UserId;
                newGroupmember.IsGroupOwner = Data.Enums.IsGroupOwner.Yes;
                newGroupmember.GroupSavingsId = newGroupSavings.Id;
                var addGroupSavingsmember = await _groupSavingsMembers.CreateSavingsGroupMembersAsync(newGroupmember);
                if(addGroupSavingsmember)
                {
                    response.Status = true;
                    response.Message = "Group account created successfully";
                    response.Data = null;
                    return response;
                }
            }
            response.Status = true;
            response.Message = "Unable to creat group account";
            response.Data = null;
            return response;
        }
        public async Task<ResponseDto<IEnumerable<GroupSavingsRespnseDto>>> GetListOfSavingsGroupAsync()
        {
            var response = new ResponseDto<IEnumerable<GroupSavingsRespnseDto>>();
            try
            {
                var result = await _groupSavingsRepository.GetListOfGroupSavingsAsync();
                if(result.Count > 0)
                {

                    response.StatusCode = 200;
                    response.DisplayMessage = "Group List Fetched successfully";
                    response.Result = result;
                    return response;

                }

                response.StatusCode = 400;
                response.DisplayMessage = "No list Available";
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 200;
                response.DisplayMessage = ex.Message;
                return response;
            }
        }
        public async Task<IEnumerable<GroupSavings>> GetListOfSavingsGroups()
        {

            var list = await _groupSavingsRepository.GetListOfGroupSavings();
            if(list.Count > 0)
            {
                return list;
            }
            return null;
        }
        public async Task<ResponseDto<GroupSavingsRespnseDto>> GetUserByIDAsync(string UserId)
        {
            var response = new ResponseDto<GroupSavingsRespnseDto>();

            try
            {
                var result = await _groupSavingsRepository.GetGroupByIdAsync(UserId);
                if(result.Result != null)
                {
                    response.StatusCode = 200;
                    response.DisplayMessage = "Group account Fetched successfully";
                    response.Result = result.Result;
                    return response;
                }

                response.StatusCode = 400;
                response.DisplayMessage = "No list Available";
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.DisplayMessage = ex.Message;
                return response;
            }

        }
        public async Task<ResponseDto<GroupSavingsRespnseDto>> GetUserByUserIDAsync(string UserId)
        {
            var response = new ResponseDto<GroupSavingsRespnseDto>();

            try
            {
                var result = await _groupSavingsRepository.GetGroupByUserIdAsync(UserId);
                if(result.Result != null)
                {
                    response.StatusCode = 200;
                    response.DisplayMessage = "Group account Fetched successfully";
                    response.Result = result.Result;
                    return response;
                }

                response.StatusCode = 400;
                response.DisplayMessage = "No list Available";
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.DisplayMessage = ex.Message;
                return response;
            }

        }
        public async Task<GroupSavings> GetGroupByID(string GroupId)
        {
            var group = await _groupSavingsRepository.GetGroupById(GroupId);
            if(group != null)
            {
                return group;
            }
            return null;

        }

    }


}

