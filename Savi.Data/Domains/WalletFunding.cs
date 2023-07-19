using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Domains
{
    public class WalletFunding : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime FundingDate { get; set; }


        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
