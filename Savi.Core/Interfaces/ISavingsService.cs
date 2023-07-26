using Savi.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Core.Interfaces
{
    public interface ISavingsService
    {
        Task<APIResponse> FundTargetSavings(int id, decimal amount);
    }
}
