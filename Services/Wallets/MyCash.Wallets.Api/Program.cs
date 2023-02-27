using MyCash.Framework;
using MyCash.Wallets.Application;
using MyCash.Wallets.Core;
using MyCash.Wallets.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMicroFramework()
    .AddCore()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
