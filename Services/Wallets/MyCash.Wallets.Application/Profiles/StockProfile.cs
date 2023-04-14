using AutoMapper;
using MyCash.PriceScraper.Core;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Entities;

namespace MyCash.Wallets.Application.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockDto>();
        CreateMap<StockDto, Stock>();

        CreateMap<GrpcStockModel, Stock>()
            .ForMember(dest => dest.Ipoyear, opt => opt.MapFrom(src => src.IpoYear))
            .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => StringToDecimal(src.MarketCap)))
            .ForMember(dest => dest.FetchData, opt => opt.MapFrom(src => src.FetchedData.ToDateTime()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.Sector))
            .ForMember(dest => dest.Change, opt => opt.MapFrom(src => StringToDecimal(src.Change)))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
            .ForMember(dest => dest.LastSale, opt => opt.MapFrom(src => StringToDecimal(src.LastSale)))
            .ForMember(dest => dest.NetChange, opt => opt.MapFrom(src => StringToDecimal(src.NetChange)))
            .ForMember(dest => dest.PctChange, opt => opt.MapFrom(src => StringToDecimal(src.PctChange)))
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => StringToDecimal(src.Volumen)));
    }

    private decimal StringToDecimal(string str)
    {
        if (!decimal.TryParse(str, out var decVal))
            return 0;
        return decVal;
    }
}
