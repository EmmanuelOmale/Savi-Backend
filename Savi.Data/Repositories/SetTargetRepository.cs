using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;
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

		public async Task<ResponseDto<SetTarget>> CreateTarget(SetTarget setTarget, string userId)
		{
			var existingTarget = await _dbContext.SetTargets.FirstOrDefaultAsync(t => t.Id == setTarget.Id);
			if(existingTarget != null)
			{
				setTarget.Id = Guid.NewGuid();

				var newExistingTarget = await _dbContext.SetTargets.FirstOrDefaultAsync(t => t.Id == setTarget.Id);
				if(newExistingTarget != null)
				{
					return new ResponseDto<SetTarget>
					{
						DisplayMessage = "An error occurred while generating a new Id.",
						StatusCode = 500,
						Result = null
					};
				}
			}


			if(!Enum.IsDefined(typeof(FrequencyType), setTarget.Frequency))
			{
				return new ResponseDto<SetTarget>
				{
					DisplayMessage = "Invalid Frequency value. Frequency must be 0, 1, or 2.",
					StatusCode = 400,
					Result = null
				};
			}
			setTarget.UserId = userId;

			_dbContext.SetTargets.Add(setTarget);
			await _dbContext.SaveChangesAsync();

			return new ResponseDto<SetTarget>
			{
				DisplayMessage = "Target added successfully.",
				StatusCode = 200,
				Result = setTarget
			};
		}

		public async Task<ResponseDto<IEnumerable<SetTarget>>> GetAllTargets()
		{
			try
			{
				var targets = await _dbContext.SetTargets.ToListAsync();
				return new ResponseDto<IEnumerable<SetTarget>>
				{
					DisplayMessage = "Targets retrieved successfully.",
					StatusCode = 200,
					Result = targets
				};
			}
			catch(Exception ex)
			{
				return new ResponseDto<IEnumerable<SetTarget>>
				{
					DisplayMessage = (ex.Message),
					StatusCode = 500,
					Result = null
				};
			}
		}

		public async Task<ResponseDto<SetTarget>> GetTargetById(Guid id)
		{
			try
			{
				var target = await _dbContext.SetTargets.FindAsync(id);
				return new ResponseDto<SetTarget>
				{
					DisplayMessage = target == null ? "Target not found." : "Target retrieved successfully.",
					StatusCode = target == null ? 404 : 200,
					Result = target
				};
			}
			catch(Exception ex)
			{
				return new ResponseDto<SetTarget>
				{
					DisplayMessage = (ex.Message),
					StatusCode = 500,
					Result = null
				};
			}
		}

		public async Task<ResponseDto<SetTarget>> UpdateTarget(Guid id, SetTarget updatedTarget)
		{
			try
			{
				var existingTarget = await _dbContext.SetTargets.FindAsync(id);

				if(existingTarget == null)
				{
					return new ResponseDto<SetTarget>
					{
						DisplayMessage = "Target not found.",
						StatusCode = 404,
						Result = null
					};
				}
				if(!Enum.IsDefined(typeof(FrequencyType), updatedTarget.Frequency))
				{
					return new ResponseDto<SetTarget>
					{
						DisplayMessage = "Invalid frequency value.",
						StatusCode = 400,
						Result = null
					};
				}
				_mapper.Map(updatedTarget, existingTarget);

				await _dbContext.SaveChangesAsync();

				return new ResponseDto<SetTarget>
				{
					DisplayMessage = "Target updated successfully.",
					StatusCode = 200,
					Result = existingTarget
				};
			}
			catch(Exception ex)
			{
				return new ResponseDto<SetTarget>
				{
					DisplayMessage = (ex.Message),
					StatusCode = 500,
					Result = null
				};
			}
		}

		public async Task<ResponseDto<SetTarget>> DeleteTarget(Guid id)
		{
			try
			{
				var target = await _dbContext.SetTargets.FindAsync(id);

				if(target == null)
				{
					return new ResponseDto<SetTarget>
					{
						DisplayMessage = "Target not found.",
						StatusCode = 404,
						Result = null
					};
				}

				_dbContext.SetTargets.Remove(target);
				await _dbContext.SaveChangesAsync();

				return new ResponseDto<SetTarget>
				{
					DisplayMessage = "Target deleted successfully.",
					StatusCode = 200,
					Result = target
				};
			}
			catch(Exception ex)
			{
				return new ResponseDto<SetTarget>
				{
					DisplayMessage = (ex.Message),
					StatusCode = 500,
					Result = null
				};
			}
		}

		public async Task<List<SetTarget>> GetSetTargetsByUserId(string userId)
		{
			return await _dbContext.SetTargets
		   .Where(t => t.UserId == userId)
		   .ToListAsync();
		}
	}
}