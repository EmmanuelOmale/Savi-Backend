using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Domains
{
    public class Bank
    {
        public string? BankCode { get; set; }
        public string BankName { get; set; } = null!;
        public string? CountryCode { get; set; }
        public int? PaystackBankId { get; set; }
    }
}
