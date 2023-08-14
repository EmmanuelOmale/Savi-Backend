using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.Enums;
using Savi.Data.IRepositories;

namespace Savi.Core.PersonalSaving
{
    public class AutoTargetFund : IAutoTargetFund
    {
        private readonly ISetTargetRepository _setTarget;
        private readonly IWalletRepository _wallet;
        private readonly IWalletDebitService _walletService;
        private readonly ISetTargetFundingService _setTargetFunding;
        private readonly IWalletCreditService _walletCredit;

        public AutoTargetFund(ISetTargetRepository setTarget, IWalletRepository wallet,
            IWalletDebitService walletService, ISetTargetFundingService setTargetFunding, IWalletCreditService walletCredit)
        {
            _setTarget = setTarget;
            _wallet = wallet;
            _walletService = walletService;
            _setTargetFunding = setTargetFunding;
            _walletCredit = walletCredit;
        }
        public async Task<bool> AutoTarget()
        {
            //Geting all the groups in GroupSavings table
            var listofTargets = await _setTarget.GetAllTargets();
            var listOfTargets = new List<SetTarget>();
            bool result = true;

            if(!listofTargets.Any())
            {
                return false;
            }
            foreach(var target in listofTargets)
            {
                var runtime = DateTime.MinValue.AddHours(target.Runtime);//Check the stipulated time to run
                var runNow = DateTime.Now.Hour;
                if(target.NextRuntime == DateTime.Today
                    && target.EndDate > DateTime.Now && runNow == runtime.Hour)
                {
                    listOfTargets.Add(target);
                }
            }
            if(listOfTargets.Count < 1)
            {
                return false;
            }
            for(int i = 0; i < listOfTargets.Count; i++)
            {
                var SetTarget = listOfTargets[i];

                var groupRun = await AutoSavings(SetTarget.Id);
                if(groupRun)
                {
                    if(SetTarget.WithdrawalDate == DateTime.Now)
                    {
                        var targetToWithdraw = await _setTarget.GetTargetById(SetTarget.Id);
                        var totalSavedAmount = targetToWithdraw.CumulativeAmount;
                        var creditUserWallet = await _walletCredit.CreditUserFundAsync(totalSavedAmount, targetToWithdraw.User.WalletId);
                        if(creditUserWallet.Status == true)
                        {
                            targetToWithdraw.CumulativeAmount = 0;
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        public async Task<bool> AutoSavings(string TargetId)
        {
            var target = await _setTarget.GetTargetById(TargetId);
            var Payment = target.AmountToSave;
            var memberwallet = await _wallet.GetUserWalletAsync(target.UserId);
            if(memberwallet.Balance < Payment)
            {
                return false;
            }
            var userwallet = memberwallet.WalletId;
            var creditmember = await _walletService.WithdrawUserFundAsync(Payment, userwallet);
            target.CumulativeAmount += Payment;
            var targetFunding = new SetTargetFunding()
            {
                Amount = Payment,
                walletId = userwallet,
                CreatedAt = DateTime.Now,
                TransactionType = TransactionType.Funding,
                SetTargetId = target.Id,
            };
            var createFunding = await _setTargetFunding.CreateTargetFund(targetFunding);
            if(createFunding)
            {
                //Updating the group next runtime.
                if(target.Frequency == FrequencyType.Daily)
                {
                    target.NextRuntime = DateTime.Today;
                    var update = await _setTarget.UpdateTarget(target);//update target
                    return true;
                }
                else if(target.Frequency == FrequencyType.Weekly)
                {
                    target.NextRuntime = DateTime.Today;
                    var update = await _setTarget.UpdateTarget(target);//update target
                    return true;
                }
                target.NextRuntime = DateTime.Now.AddDays(31);
                var updateTarget = await _setTarget.UpdateTarget(target);//update target
                return true;
            }
            return false;
        }
    }
}
