using System;
using System.Collections.Generic;
using System.Text;

namespace Savi.Data.IRepositories
{
    public interface IUnitOfWork
    {
        IIdentityTypeRepository IdentityTypeRepository { get; }
        IOccupationRepository OccupationRepository { get; }
        IUserRepository UserRepository { get; }

        void Save();
    }
}
