using EaTech.Core.Services;
using EaTech.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<INetworkService, NetworkService>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});

app.Run();
