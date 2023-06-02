using MyCash.BackgroundWorker.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApp(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseApp();

app.Run();