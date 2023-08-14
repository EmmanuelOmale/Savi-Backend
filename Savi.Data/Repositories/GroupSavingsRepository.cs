﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class GroupSavingsRepository : IGroupSavingsRepository
    {
        private readonly SaviDbContext _saviDbContext;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GroupSavingsRepository(SaviDbContext saviDbContext, IUserRepository userRepository, IMapper mapper)
        {
            _saviDbContext = saviDbContext;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateGroupSavings(GroupSavings groupSaving)
        {

            var newGroupsavings = await _saviDbContext.GroupSavings.AddAsync(groupSaving);
            var mekon = await _saviDbContext.SaveChangesAsync();
            if(mekon > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpDateGroupSavings(GroupSavings groupSaving)
        {

            var newGroupsavings = _saviDbContext.GroupSavings.Update(groupSaving);
            var mekon = await _saviDbContext.SaveChangesAsync();
            if(mekon > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<ResponseDto<GroupSavingsRespnseDto>> GetGroupByIdAsync(string Id)
        {
            var group = await _saviDbContext.GroupSavings.FindAsync(Id);
            var user = await _userRepository.GetUserById(group.UserId);
            var mapUser = _mapper.Map<UserDTO>(user);
            var mapGroup = _mapper.Map<GroupSavingsRespnseDto>(group);
            mapGroup.User = mapUser;
            if(mapGroup == null)
            {
                var notFoundResponse = new ResponseDto<GroupSavingsRespnseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    DisplayMessage = "User not found"
                };
                return notFoundResponse;
            }
            var success = new ResponseDto<GroupSavingsRespnseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                DisplayMessage = "Group Exist",
                Result = mapGroup

            };
            return success;

        }
        public async Task<ResponseDto<GroupSavingsRespnseDto>> GetGroupByUserIdAsync(string Id)
        {
            var group = await _saviDbContext.GroupSavings.FirstOrDefaultAsync(group => group.UserId == Id);
            var user = await _userRepository.GetUserById(Id);
            var mapUser = _mapper.Map<UserDTO>(user);
            var mapGroup = _mapper.Map<GroupSavingsRespnseDto>(group);
            mapGroup.User = mapUser;
            if(mapGroup == null)
            {
                var notFoundResponse = new ResponseDto<GroupSavingsRespnseDto>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    DisplayMessage = "User not found"
                };
                return notFoundResponse;
            }
            var success = new ResponseDto<GroupSavingsRespnseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                DisplayMessage = "Group Exist",
                Result = mapGroup

            };
            return success;

        }
        public async Task<ResponseDto<IEnumerable<GroupSavingsRespnseDto>>> GetListOfGroupByUserIdAsync(string Id)
        {
            var listofGroups = new List<GroupSavingsRespnseDto>();

            var group = _saviDbContext.GroupSavings.Where(x => x.UserId == Id).ToList();

            if(group == null)
            {
                var notFoundResponse = new ResponseDto<IEnumerable<GroupSavingsRespnseDto>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    DisplayMessage = "User not found"
                };
                return notFoundResponse;
            }
            var success = new ResponseDto<IEnumerable<GroupSavingsRespnseDto>>();
            foreach(var grp in group)
            {
                var user = await _userRepository.GetUserById(Id);
                var mapUser = _mapper.Map<UserDTO>(user);
                var mapGroup = _mapper.Map<GroupSavingsRespnseDto>(grp);
                mapGroup.User = mapUser;
                listofGroups.Add(mapGroup);
            }

            success.Result = listofGroups;
            success.DisplayMessage = "Success";
            success.StatusCode = 200;
            return success;
        }
        public async Task<GroupSavings> GetGroupById(string Id)
        {
            var group = await _saviDbContext.GroupSavings.FindAsync(Id);
            if(group != null)
            {
                return group;
            }
            return null;
        }

        public async Task<ICollection<GroupSavingsRespnseDto>> GetListOfGroupSavingsAsync()
        {
            var list = await _saviDbContext.GroupSavings.ToListAsync();
            if(list.Count > 0)
            {
                var listofGroups = new List<GroupSavingsRespnseDto>();
                foreach(var group in list)
                {
                    var groupowner = group.UserId;
                    var user = group.User = await _userRepository.GetUserById(groupowner);
                    var mapUser = _mapper.Map<UserDTO>(user);
                    var mapGroup = _mapper.Map<GroupSavingsRespnseDto>(group);
                    mapGroup.User = mapUser;
                    listofGroups.Add(mapGroup);

                }
                return listofGroups;
            }
            return null;


        }
        public async Task<List<GroupSavings>> GetListOfGroupSavings()
        {
            var list = await _saviDbContext.GroupSavings.ToListAsync();
            if(list.Count > 0)
            {

                return list;
            }
            return null;
        }
    }
}
