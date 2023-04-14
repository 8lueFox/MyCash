using AutoMapper;
using Google.Protobuf.WellKnownTypes;
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

        CreateMap<Stock, GrpcStockModel>()
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.IpoYear, opt => opt.MapFrom(src => src.Ipoyear))
            .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src != null ? src.MarketCap.ToString() : ""))
            .ForMember(dest => dest.LastSale, opt => opt.MapFrom(src => src != null ? src.LastSale.ToString() : ""))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Change, opt => opt.MapFrom(src => src != null ? src.Change.ToString() : ""))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
            .ForMember(dest => dest.NetChange, opt => opt.MapFrom(src => src != null ? src.NetChange.ToString() : ""))
            .ForMember(dest => dest.PctChange, opt => opt.MapFrom(src => src != null ? src.PctChange.ToString() : ""))
            .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.Sector))
            .ForMember(dest => dest.Volumen, opt => opt.MapFrom(src => src != null ? src.Volume.ToString() : ""))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.FetchedData, opt => opt.MapFrom(src => src.FetchData.ToTimestamp()));
    }
}