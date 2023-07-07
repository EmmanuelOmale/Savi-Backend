using Savi.Data.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.IRepositories
{
    public interface IIdentityTypeRepository   : IRepositoryBase<IdentityType>
    {
        void Update(IdentityType identityType);
    }
}
