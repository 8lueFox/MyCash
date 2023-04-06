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
            .ForMember(x => x.Currency, opt => opt.MapFrom(x => x.Transactions == null || x.Transactions.Count == 0 ? "Uknown" : x.Transactions.Last().Amount.Currency))
            .ForMember(x => x.Count, opt => opt.MapFrom(x => x.Transactions == null ? 0 : x.Transactions.Sum(x => x.Amount.Count)))
            .ForMember(x => x.AvgPrice, opt => opt.MapFrom(x => x.Transactions == null || x.Transactions.Count == 0 ? 0 : x.Transactions.Sum(x => x.Amount.Price * x.Amount.Count) / x.Transactions.Sum(x => x.Amount.Count)));

        CreateMap<(InvestmentObject, Stock), InvestmentObjectDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Item1.Name.Value))
            .ForMember(x => x.Currency, opt => opt.MapFrom(x => x.Item1.Transactions == null || x.Item1.Transactions.Count == 0 ? "Uknown" : x.Item1.Transactions.Last().Amount.Currency))
            .ForMember(x => x.Count, opt => opt.MapFrom(x => x.Item1.Transactions == null ? 0 : x.Item1.Transactions.Sum(x => x.Amount.Count)))
            .ForMember(x => x.AvgPrice, opt => opt.MapFrom(x => x.Item1.Transactions == null || x.Item1.Transactions.Count == 0 ? 0 : x.Item1.Transactions.Sum(x => x.Amount.Price * x.Amount.Count) / x.Item1.Transactions.Sum(x => x.Amount.Count)))
            .ForMember(x => x.CurrentValue, opt => opt.MapFrom(x => (x.Item2 == null ? 0 : x.Item2.LastSale) * x.Item1.Transactions.Sum(t => t.Amount.Count)))
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Item1.Type.Value));
    }
}
