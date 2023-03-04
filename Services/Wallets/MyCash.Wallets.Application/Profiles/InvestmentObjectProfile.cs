using AutoMapper;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Profiles;

public class InvestmentObjectProfile : Profile
{
    public InvestmentObjectProfile()
    {
        CreateMap<InvestmentObject, InvestmentObjectDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Currency, opt => opt.MapFrom(x => x.Transactions.Last().Amount.Currency))
            .ForMember(x => x.Count, opt => opt.MapFrom(x => x.Transactions.Sum(x => x.Amount.Count)))
            .ForMember(x => x.AvgPrice, opt => opt.MapFrom(x => x.Transactions.Sum(x => x.Amount.Count) / x.Transactions.Sum(x => x.Amount.Price)));
    }
}
