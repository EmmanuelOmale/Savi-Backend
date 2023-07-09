﻿using AutoMapper;
using Savi.Data.Domains;
using Savi.Data.DTO;

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
        }
    }
}