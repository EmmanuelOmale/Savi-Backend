using Savi.Data.Domains;

namespace Savi.Data.IRepositories
{
	public interface IOccupationRepository : IRepositoryBase<Occupation>
	{
		void Update(Occupation occupation);
	}
}