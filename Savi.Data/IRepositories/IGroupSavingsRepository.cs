using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
    public interface IGroupSavingsRepository
    {
        public Task<bool> CreateGroupSavings(GroupSavings groupSaving);

        public Task<ResponseDto<GroupSavingsRespnseDto>> GetGroupByIdAsync(string Id);

        public Task<bool> UpDateGroupSavings(GroupSavings groupSaving);
        public Task<GroupSavings> GetGroupById(string Id);
        public Task<List<GroupSavings>> GetListOfGroupSavings();
        Task<ICollection<GroupSavingsRespnseDto>> GetListOfGroupSavingsAsync();
        Task<ResponseDto<IEnumerable<GroupSavingsRespnseDto>>> GetListOfGroupByUserIdAsync(string Id);

        public Task<ResponseDto<GroupSavingsRespnseDto>> GetGroupByUserIdAsync(string Id);








    }

}
