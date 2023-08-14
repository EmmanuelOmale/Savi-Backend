using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class SetTargetRepository : ISetTargetRepository
    {
        private readonly SaviDbContext _dbContext;
        private readonly IMapper _mapper;
        public SetTargetRepository(SaviDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> CreateTarget(SetTarget setTarget)
        {

            var target = await _dbContext.SetTargets.AddAsync(setTarget);
            var saveTarget = await _dbContext.SaveChangesAsync();
            if(saveTarget > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<SetTarget>> GetAllTargets()
        {

            var listOfTarget = await _dbContext.SetTargets.ToListAsync();
            if(listOfTarget.Any())
            {
                return listOfTarget;
            }
            return null;
        }
        public async Task<SetTarget> GetTargetById(string TargetId)
        {

            var target = await _dbContext.SetTargets.FindAsync(TargetId);
            if(target != null)
            {
                return target;
            }
            return null;
        }
        public async Task<bool> UpdateTarget(SetTarget updatedTarget)
        {
            var updateTarget = _dbContext.SetTargets.Update(updatedTarget);
            var saveTarget = await _dbContext.SaveChangesAsync();
            if(saveTarget > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteTarget(string Id)
        {

            var targetToDelete = await _dbContext.SetTargets.FirstOrDefaultAsync(x => x.Id == Id);
            if(targetToDelete != null)
            {
                var target = _dbContext.SetTargets.Remove(targetToDelete);
                var save = await _dbContext.SaveChangesAsync();
                if(save != 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public async Task<List<SetTarget>> GetSetTargetsByUserId(string userId)
        {
            return await _dbContext.SetTargets
           .Where(t => t.UserId == userId)
           .ToListAsync();
        }
    }
}