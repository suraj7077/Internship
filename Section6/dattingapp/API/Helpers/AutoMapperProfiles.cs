using System.Linq;
using API.Entities;
using AutoMapper;
using Section6.dattingapp.API.DTOs;
using Section6.dattingapp.API.Extensions;

namespace Section6.dattingapp.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public   AutoMapperProfiles  ()
        {
            CreateMap<AppUser,MemberDto>()
            .ForMember(dest =>dest.PhotoUrl,opt =>opt.MapFrom(src =>
            src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age,opt=>opt.MapFrom(src =>src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();
        }
    }
}