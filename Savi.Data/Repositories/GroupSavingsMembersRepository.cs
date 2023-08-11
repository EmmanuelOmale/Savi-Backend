using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class GroupSavingsMembersRepository : IGroupSavingsMembersRepository
    {
        private readonly SaviDbContext _saviDbContext;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IGroupSavingsRepository _groupSavings;

        public GroupSavingsMembersRepository(SaviDbContext saviDbContext,
            IUserRepository userRepository, IMapper mapper, IGroupSavingsRepository groupSavings)
        {
            _saviDbContext = saviDbContext;
            _userRepository = userRepository;
            _mapper = mapper;
            _groupSavings = groupSavings;
        }



        public async Task<bool> CreateSavingsGroupMembersAsync(GroupSavingsMembers groupSavingsMembers)
        {
            var newMember = await _saviDbContext.GroupSavingsMembers.AddAsync(groupSavingsMembers);
            var result = await _saviDbContext.SaveChangesAsync();
            if(result > 0)
            {
                return true;

            }
            return false;
        }

        public async Task<int> GetListOfGroupMembersAsync(string GroupId)
        {
            var list = await _saviDbContext.GroupSavingsMembers.Where(x => x.GroupSavingsId == GroupId).ToListAsync();
            if(list.Count > 0)
            {
                return list.Count;
            }
            return 0;
        }
        public async Task<List<GroupSavingsMembers>> GetListOfGroupMembersAsync2(string GroupId)
        {
            var list = await _saviDbContext.GroupSavingsMembers.Where(x => x.GroupSavingsId == GroupId).ToListAsync();
            if(list.Count > 0)
            {
                return list;
            }
            return null;
        }
        public async Task<List<GroupMembersDto>> GetListOfGroupMembersAsync3(string GroupId)
        {
            var list = await _saviDbContext.GroupSavingsMembers.Where(x => x.GroupSavingsId == GroupId).ToListAsync();
            var list2 = new List<GroupMembersDto>();
            if(list.Count > 0)
            {
                foreach(var item in list)
                {
                    var user = await _userRepository.GetUserById(item.UserId);
                    var mapUser = _mapper.Map<UserDTO>(user);
                    var mapGroup = _mapper.Map<GroupMembersDto>(item);
                    mapGroup.User = mapUser;
                    list2.Add(mapGroup);


                }
                return list2;
            }
            return null;
        }

        public async Task<int> GetUserLastUserPosition()
        {
            int highestPosition = await _saviDbContext.GroupSavingsMembers.OrderByDescending(user => user.Positions)
                              .Select(user => user.Positions)
                              .FirstOrDefaultAsync();
            return highestPosition;
        }
        public async Task<List<int>> GetUserFirstUserPosition()
        {
            var highestPosition = await _saviDbContext.GroupSavingsMembers.OrderByDescending(user => user.Positions)
                               .Select(user => user.Positions)
                               .ToListAsync();
            return highestPosition;
        }
        public bool Check_If_UserExist(string UserId)
        {
            var userExist = _saviDbContext.GroupSavingsMembers.FirstOrDefault(x => x.UserId == UserId);

            if(userExist != null)
            {
                return true;
            }
            return false;
        }




    }
}
