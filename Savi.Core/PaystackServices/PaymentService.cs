using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;
using System.Text.Json;

namespace Savi.Core.PaystackServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletFundingRepository _walletFunding;

        public PaymentService(IHttpClientFactory httpClientFactory, IWalletRepository walletRepository, SaviDbContext saviDb, IWalletFundingRepository walletFundingRepository)
        {
            _httpClientFactory = httpClientFactory;
            _walletRepository = walletRepository;
            _walletFunding = walletFundingRepository;
        }
        public async Task<PayStackResponseDto> VerifyPaymentAsync(string reference)
        {


            try
            {
                if (string.IsNullOrWhiteSpace(reference))
                {
                    throw new Exception("Please provide a valid reference number");
                }

                var client = _httpClientFactory.CreateClient("Paystack");

                string apiUrl = $"transaction/verify/{reference}";

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonElement>(content);

                    // Checking if responseData is not null 
                    if (responseData.ValueKind == JsonValueKind.Object)
                    {
                        // Geting specific properties from the JSON object
                        var data = responseData.GetProperty("data");
                        decimal amount = data.GetProperty("amount").GetDecimal();
                        var phone = data.GetProperty("customer").GetProperty("phone").GetString();

                        var walletId = new Wallet();
                        var UserWalletId = walletId.SetWalletId(phone);

                        // Updating local database with the payment information.

                        var entry = await _walletRepository.GetWalletByPhoneNumber(UserWalletId);
                        if (entry != null)
                        {
                            var newbalance = entry.Balance += amount;
                            var walletentry = await _walletRepository.VerifyPaymentAsync(entry);
                            if (walletentry)
                            {
                                //Creating new walletfunding record for the transaction
                                var newWalletfund = new WalletFunding()
                                {
                                    Amount = amount,
                                    WalletId = UserWalletId,
                                    TransactionType = Data.Enums.TransactionType.Funding,
                                    CreatedAt = DateTime.UtcNow,
                                    Description = "Credit",
                                    Cummulative = newbalance,

                                };
                                var newwalletfunding = await _walletFunding.CreateFundingWalletAsync(newWalletfund);
                                if (newwalletfunding)
                                {
                                    var result0 = new PayStackResponseDto()
                                    {
                                        Status = true,
                                        Message = ($"Payment Verified!You have successfully credited {newWalletfund.Amount}, your wallet balance is {newbalance}"),
                                        Data = data.GetProperty("customer")
                                    };
                                    return result0;
                                }
                                var result1 = new PayStackResponseDto()
                                {
                                    Status = true,
                                    Message = ("Payment Verified! error encountered trying to update walletfunding"),
                                    Data = data.GetProperty("customer")
                                };
                                return result1;


                            }
                            var result = new PayStackResponseDto()
                            {
                                Status = true,
                                Message = ("Payment Verified! error encountered trying to update wallet"),
                                Data = data.GetProperty("customer")
                            };
                            return result;


                        }
                        var result2 = new PayStackResponseDto()
                        {
                            Status = true,
                            Message = ("Payment Unverified! error tying to verify walletId"),
                            Data = null
                        };
                        return result2;

                    }

                    var result31 = new PayStackResponseDto()
                    {
                        Status = true,
                        Message = ("Payment Unverified! error trying to deserialize data"),
                        Data = null
                    };
                    return result31;


                }

                var result3 = new PayStackResponseDto()
                {
                    Status = true,
                    Message = ("Payment Unverified! error trying to initiate paystack confirm payment reference and try again"),
                    Data = null
                };
                return result3;



            }
            catch (Exception ex)
            {
                var result4 = new PayStackResponseDto()
                {
                    Status = true,
                    Message = ex.Message,
                    Data = null,
                };
                return result4;
            }
        }

        public async Task<PayStackResponseDto> WithdrawFundAsync(decimal amount, string walletId)
        {
            try
            {
                var UserWallet = await _walletRepository.GetWalletByPhoneNumber(walletId);
                if (UserWallet != null)
                {
                    var walletbalance = await _walletRepository.GetBalanceAsync(walletId);
                    if (amount < walletbalance && amount > 0)
                    {
                        var newbalance = walletbalance - amount;
                        var updatedWallet = new Wallet()
                        {
                            WalletId = walletId,
                            Balance = newbalance.Value
                        };
                        var walletupdate = _walletRepository.VerifyPaymentAsync(updatedWallet);
                        var newWalletfunding = new WalletFunding()
                        {
                            WalletId = walletId,
                            Amount = amount,
                            TransactionType = Data.Enums.TransactionType.Withdrawal,
                            Description = "Debit",
                            Cummulative = newbalance.Value,


                        };
                        var createnewWalletFund = await _walletFunding.CreateFundingWalletAsync(newWalletfunding);
                        var result31 = new PayStackResponseDto()
                        {
                            Status = true,
                            Message = ($"You have successfully withdraw {amount}, you have {newbalance} left"),
                            Data = null
                        };
                        return result31;
                    }
                    var result2 = new PayStackResponseDto()
                    {
                        Status = false,
                        Message = ("Insufficient balance"),
                        Data = null
                    };
                    return result2;
                }
                var result3 = new PayStackResponseDto()
                {
                    Status = false,
                    Message = ("Invalid Credentials"),
                    Data = null
                };
                return result3;
            }
            catch (Exception ex)
            {
                var result3 = new PayStackResponseDto()
                {
                    Status = false,
                    Message = ex.Message,
                    Data = null
                };
                return result3;
            }

        }

    }

}
