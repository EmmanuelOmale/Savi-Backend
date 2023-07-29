﻿using Savi.Data.Domains;
using Savi.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Core.Interfaces
{
    public interface IWalletService
    {
        Task<APIResponse> DebitWallet(string walletId, decimal amount);

    }
}