using Savi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.DTO
{
	public class SetTargetFundingDTO
	{
		public decimal Amount { get; set; }
		public decimal CummulativeAmount { get; set; }
		public TransactionType TransactionType { get; set; }
		public Guid SetTargetId { get; set; }

	}
}
