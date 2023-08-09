using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
	public class IdentityTypeRepository : RepositoryBase<IdentityType>, IIdentityTypeRepository
	{
		private SaviDbContext _saviDbContext;

		public IdentityTypeRepository(SaviDbContext db) : base(db)
		{
			_saviDbContext = db;
		}

		public void Update(IdentityType identityType)
		{
			_saviDbContext.Entry(identityType).State = EntityState.Modified;
			_saviDbContext.SaveChanges();
		}
	}
}