using AutoMapper;
using MyCash.Users.Core.Entities;
using MyCash.Users.Core.Services;

namespace MyCash.Users.Core.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GrpcUserModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(s => s.Id))
            .ForMember(dest => dest.Package, opt => opt.MapFrom(s => s.Package.Value));
    }
}