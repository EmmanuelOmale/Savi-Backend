using Savi.Data.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.IRepositories
{
    public interface IOccupationRepository  : IRepositoryBase<Occupation>
    {
        void Update(Occupation occupation);
    }
}
