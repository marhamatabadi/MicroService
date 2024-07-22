using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<PlatformReadDto, Platform>().ReverseMap();
            CreateMap<PlatformEditDto, Platform>().ReverseMap();
            CreateMap<PlatformCreateDto, Platform>().ReverseMap();
        }
    }
}
