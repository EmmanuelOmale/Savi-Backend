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
            var result = await _groupSavingsRepository.CreateGroupSavings(newGroupSavings);
            if (result)
            {
                var newGroupmember = new GroupSavingsMembers();
                newGroupmember.Positions = newGroupmember.Positions;
                await _userRepository.GetUserByIdAsync(groupSavingsDto.UserId);
                newGroupmember.UserId = groupSavingsDto.UserId;
                newGroupmember.IsGroupOwner = Data.Enums.IsGroupOwner.Yes;
                newGroupmember.GroupSavingsId = newGroupSavings.Id;
                var addGroupSavingsmember = await _groupSavingsMembers.CreateSavingsGroupMembersAsync(newGroupmember);
                if (addGroupSavingsmember)
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


        //public async Task<bool> ActivateGroupSavings(string GroupId)
        //{
        //    var response = await _groupSavingsRepository.GetGroupByIdAsync(GroupId);
        //    {
        //        if (response.Result != null)
        //        {
        //            var grp = await _groupSavingsMembers.GetListOfGroupMembersAsync2(GroupId);
        //            var gr = grp.Count();

        //            for (int i = 0; i <= gr; i++)
        //            {
        //                var rem = grp.First();
        //                var user = await _userRepository.GetUserByIdAsync(rem.UserId);

        //                if (rem.Positions == 0)
        //                {
        //                    var contributions = response.Result.ContributionAmount;
        //                    var members = response.Result.MemberCount;
        //                    var totalAmountContributed = (members - 1) * contributions;
        //                    var newcrediteduser = await _walletServices.CreditUserFundAsync(totalAmountContributed, user.Result.WalletId);
        //                    if (newcrediteduser.Status == true)
        //                    {
        //                        return true;
        //                    }

        //                }
        //                else if (rem.Positions != 0)
        //                {
        //                    var contribution = response.Result.ContributionAmount;
        //                    var newDeediteduser = await _walletServices.WithdrawUserFundAsync(contribution, user.Result.WalletId);
        //                }




        //            }
        //        }
        //        return false;
        //    }





        //public async Task<bool> AutoDebitAndCredit()
        //{
        //    var Groups = await _groupSavingsRepository.GetListOfGroupSavingsAsync();
        //    var gr = Groups.Count();

        //    for(int i = 0; i <= gr; i++)
        //    {
        //        var rem = Groups.First();
        //        if(rem.ActualStartDate == DateTime.Now)
        //        {
        //            var grp =await _groupSavingsMembers.GetListOfGroupMembersAsync(rem.Id);
        //            var usr = 
        //            _walletServices.CreditUserFundAsync()
        //        }
        //    }
        //}


    }
}
