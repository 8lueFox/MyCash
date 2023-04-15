//Mappers to benchmark tests:
//AutoMapper, TinyMapper, Mapster, Mapperly 

using BenchmarkDotNet.Running;
using MyCash.Tests.Benchmarks.Mapper;

BenchmarkRunner.Run<Benchmarks>();