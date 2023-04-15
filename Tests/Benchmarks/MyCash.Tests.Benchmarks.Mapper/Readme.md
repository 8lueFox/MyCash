----- Summary -----

BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1555/22H2/2022Update/SunValley2)
AMD Ryzen 5 3600XT, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.203
  [Host]     : .NET 6.0.16 (6.0.1623.17311), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.16 (6.0.1623.17311), X64 RyuJIT AVX2


|           Method |         Mean |      Error |       StdDev | Allocated |
|----------------- |-------------:|-----------:|-------------:|----------:|
|   MapperlySingle |     21.28 ns |   0.245 ns |     0.205 ns |     240 B |
| TinyMapperSingle |     49.01 ns |   0.969 ns |     1.190 ns |     240 B |
|    MapsterSingle |     49.59 ns |   1.019 ns |     1.090 ns |     240 B |
| AutoMapperSingle |    105.53 ns |   2.007 ns |     3.297 ns |     240 B |
|      MapsterList | 27,623.33 ns | 544.637 ns |   605.363 ns |  256640 B |
|     MapperlyList | 28,883.56 ns | 286.253 ns |   253.756 ns |  248216 B |
|   AutoMapperList | 30,092.89 ns | 594.384 ns |   555.988 ns |  264697 B |
|   TinyMapperList | 49,040.96 ns | 895.406 ns | 1,164.280 ns |  264696 B |