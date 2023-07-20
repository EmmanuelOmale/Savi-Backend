using Savi.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Savi.Data.Domains
{
    public class WalletFunding : BaseEntity
    {
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string Narration { get; set; } 
        public string TransactionCode { get; set; }
        public bool Status { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }

        [ForeignKey("WalletId")]
        public string WalletId { get; set; } 
        public Wallet Wallet { get; set; } 
       
    }
}
