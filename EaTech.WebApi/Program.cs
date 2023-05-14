using EaTech.Services;
using EaTech.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IGraphService, GraphService>();

var app = builder.Build();

app.UseSwagger();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
    endpoints.MapControllerRoute("default", "{controller=App}/{action=Index}");
});

app.Run();
