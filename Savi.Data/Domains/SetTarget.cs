using Savi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Domains
{
    public class SetTarget
    {
        public Guid Id { get; set; }
        public string Target { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal AmountToSave { get; set; }
        public FrequencyType Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime WithdrawalDate { get; set; }
		public SetTargetFunding SetTargetFunding { get; set; }
		public decimal CumulativeAmount { get; set; }
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }


	}
}
