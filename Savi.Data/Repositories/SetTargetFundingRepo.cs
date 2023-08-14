using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class SetTargetFundingRepo : ITargetFundingRepo
    {
        private readonly SaviDbContext _saviDbContext;
        private readonly IWalletRepository _wallet;
        private readonly IMapper _mapper;
        private readonly ISetTargetRepository _setTarget;
        public SetTargetFundingRepo(SaviDbContext saviDbContext, IWalletRepository wallet, IMapper mapper, ISetTargetRepository setTarget)
        {
            _saviDbContext = saviDbContext;
            _wallet = wallet;
            _mapper = mapper;
            _setTarget = setTarget;
        }
        public async Task<bool> CreateTargetFundingAsync(SetTargetFunding funding)
        {
            var entry = await _saviDbContext.SetTargetFundings.AddAsync(funding);
            int rowsAffected = await _saviDbContext.SaveChangesAsync();

            if(rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<SetTargetFundingDTO>> GetListOfTargetFundingAsync(string UserId)
        {
            var listofTarget = new List<SetTargetFundingDTO>();
            var wallet = await _wallet.GetUserWalletAsync(UserId);
            var target = await _saviDbContext.SetTargetFundings
           .Where(t => t.walletId == wallet.WalletId)
           .ToListAsync();
            foreach(var targetFunding in target)
            {
                var setTarget = targetFunding.SetTarget = await _setTarget.GetTargetById(targetFunding.SetTargetId);
                var set = _mapper.Map<SetTargetDTO>(setTarget);
                var targetdto = _mapper.Map<SetTargetFundingDTO>(targetFunding);
                targetdto.SetTarget = set;
                listofTarget.Add(targetdto);
            }
            return listofTarget;

        }
    }
}
