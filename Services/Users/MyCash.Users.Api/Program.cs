using Micro.Framework;
using MyCash.Users.Core;
using MyCash.Users.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMicroFramework(builder.Configuration);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddGrpc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcUserService>();
app.MapGet("protos/users.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("../MyCash.Users.Core/Protos/users.proto"));
});

app.Run();
