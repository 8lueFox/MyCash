using AutoMapper;
using MyCash.WealthManager.Application.DTO;
using MyCash.WealthManager.Core.Entities;

namespace MyCash.WealthManager.Application.Profiles;

public class FamilyProfile : Profile
{
    public FamilyProfile()
    {
        CreateMap<Family, FamilySummaryDto>()
            .ForMember(x => x.ExpectedMonthyExpenses, opt => opt.MapFrom(x => x.Settings.ExpectedMonthyExpenses))
            .ForMember(x => x.SumOfExpenses, opt => opt.MapFrom(x => x.GetSumOfExpenses(x.Settings.Currency)))
            .ForMember(x => x.SumOfIncomes, opt => opt.MapFrom(x => x.GetSumOfIncomes(x.Settings.Currency)))
            .ForMember(x => x.Colour, opt => opt.MapFrom(x => x.Settings.Colour))
            .ForMember(x => x.Currency, opt => opt.MapFrom(x => x.Settings.Currency))
            .ForMember(x => x.FamilyName, opt => opt.MapFrom(x => x.FamilyName));
    }
}
