using Savi.Data.Domains;

namespace Savi.Data.IRepositories
{
	public interface IIdentityTypeRepository : IRepositoryBase<IdentityType>
	{
		void Update(IdentityType identityType);
	}
}