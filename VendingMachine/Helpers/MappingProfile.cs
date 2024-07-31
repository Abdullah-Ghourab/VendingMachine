using AutoMapper;
using VendingMachine.Core.DTOs;
using VendingMachine.Core.Models;

namespace VendingMachine.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
