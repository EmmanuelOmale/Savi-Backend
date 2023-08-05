using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.Enums;
using Savi.Data.IRepositories;

namespace Savi.Core.WalletService
{
    public class GroupWalletFundingServices : IGroupWalletFundingServices
    {
        private readonly IGroupSavingsMembersRepository _groupSavingsMembers;
        private readonly IGroupSavingsRepository _groupSavingsRepository;
        private readonly IWalletCreditService _walletCreditService;
        private readonly IUserRepository _userRepository;
        private readonly IGroupsavingsFundingRepository _groupsavingsFundingRepository;
        private readonly IWalletDebitService _walletDebitService;
        private readonly IGroupFund _groupFund;
        public GroupWalletFundingServices(IGroupSavingsMembersRepository groupSavingsMembers,
            IGroupSavingsRepository groupSavingsRepository, IWalletCreditService walletCreditService,
            IUserRepository userRepository, IGroupsavingsFundingRepository groupsavingsFundingRepository,
            IWalletDebitService walletDebitService, IGroupFund groupFund)
        {
            _groupSavingsMembers = groupSavingsMembers;
            _groupSavingsRepository = groupSavingsRepository;
            _walletCreditService = walletCreditService;
            _userRepository = userRepository;
            _groupsavingsFundingRepository = groupsavingsFundingRepository;
            _walletDebitService = walletDebitService;
            _groupFund = groupFund;
        }
        public async Task<bool> GroupAuto()
        {
            //Geting all the groups in GroupSavings table
            var listofGroupsavings = await _groupSavingsRepository.GetListOfGroupSavings();
            foreach(var group in listofGroupsavings)
            {
                if(group.GroupStatus != GroupStatus.Running || group.NextRunTime != DateTime.Today)//Checking the status and NextRunTime
                {
                    return false;
                }
                if(group.Count <= group.MemberCount)//Checking the group is eligible to run again.
                {
                    var groupRun = await AutoGroupSavings(group.Id);
                    if(groupRun.Equals(true))
                    {
                        return true;
                    }
                    return false;
                }
                return false;

            };
            return true;
        }

        public async Task<bool> AutoGroupSavings(string GroupId)
        {

            var group = await _groupSavingsRepository.GetGroupById(GroupId);
            var runtime = DateTime.MinValue.AddHours(group.Runtime);//Check the stipulated time to run
            var runNow = DateTime.Now.Hour;
            if(runtime.Hour == runNow)//Compare runtime and runNow
            {
                //Finding the Groupmember to credit
                var frequency = group.FrequencyId;
                var members = await _groupSavingsMembers.GetListOfGroupMembersAsync2(GroupId);
                int positions = group.Count;
                var memberCounts = members.Count;
                var Payment = group.ContributionAmount * (memberCounts - 1);
                var memberTofund = members.FirstOrDefault(x => x.Positions == positions);
                var memberwallet = await _userRepository.GetUserById(memberTofund?.UserId);
                var userwallet = memberwallet.WalletId;
                var creditmember = await _walletCreditService.CreditUserFundAsync(Payment, userwallet);
                var groupSavingsFunding = new GroupSavingsFunding()
                {
                    GroupSavingsId = GroupId,
                    Amount = Payment,
                    UserId = memberTofund?.UserId,
                    TransactionType = TransactionType.Funding
                };
                var createFunding = await _groupsavingsFundingRepository.CreateGroupSavingsFundingAsync(groupSavingsFunding);
                if(memberCounts == group.Count)
                {
                    group.GroupStatus = GroupStatus.Completed;
                }
                if(createFunding)
                {
                    //If credited then debit the rest members
                    group.Count += 1;//Increment the group count
                    group.ModifiedAt = DateTime.UtcNow;
                    await _groupSavingsRepository.UpDateGroupSavings(group);//update group

                    for(int i = 0; i < memberCounts; i++)
                    {
                        var m = members[i];
                        if(m.Positions != positions)
                        {
                            var user = await _userRepository.GetUserByIdAsync(m.UserId);
                            var userWalletId = user.Result.WalletId;
                            var amountToDebit = group.ContributionAmount;
                            var debitusers = await _walletDebitService.WithdrawUserFundAsync(amountToDebit, userWalletId);
                            if(debitusers.Status == true)
                            {
                                var groupSavingsWithdraw = new GroupSavingsFunding()
                                {
                                    GroupSavingsId = GroupId,
                                    Amount = amountToDebit,
                                    UserId = m.UserId,
                                    TransactionType = TransactionType.Withdrawal
                                };
                                var debitFund = await _groupFund.GroupFundingMember(groupSavingsWithdraw);
                            }
                        }
                    }
                    //Updating the group next runtime.
                    if(group.FrequencyId == 1)
                    {
                        group.NextRunTime = DateTime.Today;
                        await _groupSavingsRepository.UpDateGroupSavings(group);
                        return true;
                    }
                    else if(group.FrequencyId == 2)
                    {
                        group.NextRunTime = DateTime.Now.AddDays(7);
                        await _groupSavingsRepository.UpDateGroupSavings(group);
                        return true;

                    }
                    group.NextRunTime = DateTime.Now.AddDays(31);
                    await _groupSavingsRepository.UpDateGroupSavings(group);
                    return true;
                }
                return false;
            }

            return false;

        }
    }
}
