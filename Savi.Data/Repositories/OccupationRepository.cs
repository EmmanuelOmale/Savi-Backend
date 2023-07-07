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
    public class OccupationRepository  : RepositoryBase<Occupation>, IOccupationRepository
    {
        private SaviDbContext _saviDbContext;

        public OccupationRepository(SaviDbContext db) : base(db)
        {
            _saviDbContext = db;
        }

        public void Update(Occupation occupation)
        {
            _saviDbContext.Entry(occupation).State = EntityState.Modified;
            _saviDbContext.SaveChanges();
        }
    }
}
