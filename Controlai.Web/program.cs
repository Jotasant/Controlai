using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ConnectString.json", optional: false, reloadOnChange: true);

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
