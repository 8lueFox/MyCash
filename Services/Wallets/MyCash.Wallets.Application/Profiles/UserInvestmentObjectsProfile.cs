using AutoMapper;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Profiles;

public class UserInvestmentObjectsProfile : Profile
{
    public UserInvestmentObjectsProfile()
    {
        CreateMap<UserInvestmentObjects, UserInvestmentObjectsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.UserInvestmentObjectName))
            .ForMember(dest => dest.TotalSum,
                opt => opt.MapFrom(x => x.InvestmentObjects.Sum(x => x.Transactions.Sum(y => y.Amount.Count * y.Amount.Price))));
    }
}
