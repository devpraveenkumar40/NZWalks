using AutoMapper;
using NZWalksAPI.Models.Dtos;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Region Mapping
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<CreateRegionDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();

            // Difficulty Mapping
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<CreateDifficultyDto, Difficulty>().ReverseMap();
            CreateMap<UpdateDifficultyDto, Difficulty>().ReverseMap();

            // Walk Mapping 
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<AddWalkRequestDto,Walk>().ReverseMap();
            CreateMap<UpdateWalkDto,Walk>().ReverseMap();
        }
    }
}
