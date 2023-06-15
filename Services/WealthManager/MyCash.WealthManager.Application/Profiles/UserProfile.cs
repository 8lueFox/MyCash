using AutoMapper;
using MyCash.WealthManager.Application.DTO;
using MyCash.WealthManager.Core;
using MyCash.WealthManager.Core.Entities;
using MyCash.WealthManager.Core.ValueObjects;

namespace MyCash.WealthManager.Application.Profiles;

internal class UserProfile : Profile
{
    public UserProfile()
    {
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
