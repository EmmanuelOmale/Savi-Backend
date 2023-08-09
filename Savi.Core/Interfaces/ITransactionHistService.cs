using Savi.Data.Domains;
using Savi.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Core.Interfaces
{
	public interface ITransactionHistService
	{
		IQueryable<UserTransaction> GetOldestUserTransactions(string userId, int page);
		IQueryable<UserTransaction> GetRecentUserTransactions(string userId, int page);
		IQueryable<UserTransaction> GetMoreUserTransactions(string userId, int page);



	}
}
