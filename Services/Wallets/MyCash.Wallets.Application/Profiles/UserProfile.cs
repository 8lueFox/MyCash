using AutoMapper;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Profiles;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserPackage, opt => opt.MapFrom(src => src.UserPackage.Value));
    }
}
