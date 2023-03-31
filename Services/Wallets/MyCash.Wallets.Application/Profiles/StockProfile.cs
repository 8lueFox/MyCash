using AutoMapper;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockDto>();
        CreateMap<StockDto, Stock>();
    }
}
