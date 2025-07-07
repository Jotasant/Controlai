using Microsoft.Extensions.Configuration;
using Infraestrutura.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ConnectString.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped<Infraestrutura.Data.ConexaoRepo>();

builder.Services.AddControllers();

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Requisição: {context.Request.Path}");
    await next();
});

app.UseAuthentication();

app.UseRouting();

app.MapControllers();

app.Run();
