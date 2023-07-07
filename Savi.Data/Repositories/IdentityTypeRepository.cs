using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
