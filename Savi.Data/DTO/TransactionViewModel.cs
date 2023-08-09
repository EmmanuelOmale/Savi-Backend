using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.DTO
{
	public class TransactionViewModel
	{
		public string ReceiverName { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
	}

}
