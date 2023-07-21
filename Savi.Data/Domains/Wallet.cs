using Savi.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Domains
{
    public class Wallet : BaseEntity
    {
        public string WalletId { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public string Reference { get; set; }
        public string Pin { get; set; } 
        public string Code { get; set; } 
        public string PaystackCustomerCode { get; set; }
        public void SetWalletId(string phoneNumber)
        {
            if (phoneNumber.StartsWith("+234"))
            {
                phoneNumber = phoneNumber.Substring(4); // Remove '+234'
            }
            else if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = phoneNumber.Substring(1); // Remove leading '0'
            }

            if (phoneNumber.Length == 10 && long.TryParse(phoneNumber, out long walletId))
            {
                WalletId = walletId.ToString();
            }
            else
            {
                throw new Exception("Invalid Phone Number Format");
            }
        }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string WalletFundingId { get; set; }
        public ICollection<WalletFunding> WalletFunding { get; set; } 

    }

}
