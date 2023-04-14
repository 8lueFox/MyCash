using Micro.Framework;
using MyCash.PriceScraper.Core;
using MyCash.PriceScraper.Core.Services;
using MyCash.PriceScraper.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMicroFramework(builder.Configuration)
    .AddCore()
    .AddInfrastructure();
builder.Services.AddGrpc();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseInfrastructure();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcStockService>();
app.MapGet("protos/stocks.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("../MyCash.PriceScraper.Core/Protos/stocks.proto"));
});

app.Run();
