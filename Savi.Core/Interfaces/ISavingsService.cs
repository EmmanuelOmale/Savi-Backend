using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
	public interface ISavingsService
	{
		Task<APIResponse> FundTargetSavings(string id, decimal amount, string userId);
	}
}