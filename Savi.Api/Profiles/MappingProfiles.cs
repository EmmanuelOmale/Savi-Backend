using AutoMapper;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;

namespace Savi.Api.Profiles
{
    public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<IdentityType, CreateIdentityDto>().ReverseMap();
			CreateMap<Occupation, CreateOccupationDto>().ReverseMap();
			CreateMap<IdentityType, UpdateIdentityDto>().ReverseMap();
			CreateMap<Occupation, UpdateOccupationDto>().ReverseMap();
			CreateMap<ApplicationUser, UserDTO>().ReverseMap();
			CreateMap<SetTargetFunding, SetTargetFundingDTO>().ReverseMap();
			CreateMap<SetTarget, SetTargetDTO>().ReverseMap();
			CreateMap<GroupSavings, GroupSavingsDto>().ReverseMap();
			CreateMap<GroupSavings, GroupSavingsRespnseDto>().ReverseMap();
			CreateMap<GroupSavingsMembers, GroupMembersDto>().ReverseMap();
      CreateMap<KYC, AddKycDto>().ReverseMap();
	}
}