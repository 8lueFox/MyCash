using Micro.Framework;
using MyCash.PriceScraper.Core;
using MyCash.PriceScraper.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMicroFramework(builder.Configuration)
    .AddCore()
    .AddInfrastructure();

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

app.Run();
