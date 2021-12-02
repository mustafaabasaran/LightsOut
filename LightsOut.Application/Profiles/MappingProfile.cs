using AutoMapper;
using LightsOut.Application.DTOs;
using LightsOut.Domain.Models;

namespace LightsOut.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BoardSettingDto, BoardSetting>().ReverseMap();
            CreateMap<InitialState, InitialStateDto>().ReverseMap();
        }
    }
}