﻿using Savi.Data.Context;
using Savi.Data.IRepositories;
using Savi.Data.Repositories;

namespace Savi.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private SaviDbContext _saviDbContext;

        public IIdentityTypeRepository IdentityTypeRepository { get; private set; }

        public IOccupationRepository OccupationRepository { get; private set; }

        public UnitOfWork(SaviDbContext saviDbContext)
        {
            _saviDbContext = saviDbContext;
            OccupationRepository = new OccupationRepository(_saviDbContext);
            IdentityTypeRepository = new IdentityTypeRepository(_saviDbContext);
        }
    

        public void Save()
        {
              _saviDbContext.SaveChanges();
        }
    }
}