using AutoMapper;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.ValueObjects;

namespace MyCash.Wallets.Application.Profiles;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserPackage, opt => opt.MapFrom(src => src.UserPackage.Value));

        CreateMap<UserBusDto, User>()
            .ConstructUsing(x => new User(Guid.NewGuid(), x.Id, MapUserPackage(x.UserPackage)));

        CreateMap<GrpcUserModel, User>()
            .ConstructUsing(x => new User(Guid.Parse(x.UserId), Guid.Parse(x.UserId), MapUserPackage(x.Package)));
    }

    private static UserPackage MapUserPackage(string userPackage)
        => userPackage switch
        {
            "Standard" => UserPackage.Standard,
            "Premium" => UserPackage.Premium,
            _ => UserPackage.None
        };
}
