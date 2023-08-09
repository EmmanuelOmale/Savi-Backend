using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;

namespace Savi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionHistoryController : ControllerBase
	{
		ITransactionHistService _transHist;

		public TransactionHistoryController(ITransactionHistService transHist)
		{
			_transHist = transHist;
		}

		[HttpGet("recently")]
		public IActionResult GetRecentTransaction(string userId,int page)
		{
			var recent = _transHist.GetRecentUserTransactions(userId, page);
			return Ok(recent);
		}
		[HttpGet("old")]
		public IActionResult GetOldTransaction(string userId, int page)
		{
			var recent = _transHist.GetOldestUserTransactions(userId, page);
			return Ok(recent);
		}
		[HttpGet("more")]
		public IActionResult GetMoreTransaction(string userId, int page)
		{
			var recent = _transHist.GetMoreUserTransactions(userId, page);
			return Ok(recent);
		}
	}
}
