using AutoMapper;
using MyCash.PriceScraper.Core.Dtos;
using MyCash.PriceScraper.Core.Entities;
using MyCash.PriceScraper.Core.Queries;

namespace MyCash.PriceScraper.Core.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Row, Stock>()
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.symbol))
            .ForMember(dest => dest.Ipoyear, opt => opt.MapFrom(src => src.ipoyear))
            .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.marketCap == "" ? null : src.marketCap.Replace('.', ',')))
            .ForMember(dest => dest.LastSale, opt => opt.MapFrom(src => src.lastsale == "" ? null : src.lastsale.Trim('$').Replace('.', ',')))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Change, opt => opt.MapFrom(src => src.netchange == "" ? null : src.netchange.Replace('.', ',')))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
            .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.industry))
            .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.marketCap == "" ? null : src.marketCap.Replace('.', ',')))
            .ForMember(dest => dest.NetChange, opt => opt.MapFrom(src => src.netchange == "" ? null : src.netchange.Replace('.', ',')))
            .ForMember(dest => dest.PctChange, opt => opt.MapFrom(src => src.pctchange == "" ? null : src.pctchange.Trim('%').Replace('.', ',')))
            .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.sector))
            .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.volume))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.url));

        CreateMap<Stock, StockDto>();
    }
}