using Savi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Savi.Data.Domains
{
    public class WalletFunding : BaseEntity
    {
        public decimal Amount { get; set; }
        public string Reference { get; set; } = null!;
        public string Narration { get; set; } = null!;
        public string? TransactionCode { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = null!;
        public string WalletId { get; set; } = null!;
        public Wallet Wallet { get; set; } = null!; 
       
    }
}
