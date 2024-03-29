using Micro.Framework;
using MyCash.WealthManager.Application;
using MyCash.WealthManager.Core;
using MyCash.WealthManager.Infrastructure;
using Extensions = MyCash.WealthManager.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMicroFramework(builder.Configuration)
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Extensions.InitDb(builder.Services.BuildServiceProvider(), new CancellationToken());

app.Run();
