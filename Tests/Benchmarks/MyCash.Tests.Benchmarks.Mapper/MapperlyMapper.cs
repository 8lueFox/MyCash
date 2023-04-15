using MyCash.Tests.Benchmarks.Mapper.Models;
using Riok.Mapperly.Abstractions;

namespace MyCash.Tests.Benchmarks.Mapper;

[Mapper]
public partial class MapperlyMapper
{
    public partial Stock Map(StockDto stockDto);
    public partial IEnumerable<Stock> Map(IEnumerable<StockDto> stockDto);
}
