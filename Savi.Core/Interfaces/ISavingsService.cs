using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
	public interface ISavingsService
	{
		Task<APIResponse> FundTargetSavings(Guid id, decimal amount, string userId);
	}
}