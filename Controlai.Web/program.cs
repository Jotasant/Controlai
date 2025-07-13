using Microsoft.Extensions.Configuration;

using Applicacao.Servicos;
using Dominio.Interfaces;
using Infraestrutura.Repositorio;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ConnectString.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddScoped<IRepoUsuarioSistema, RepoUsuarioSistema>();
builder.Services.AddScoped<SvcObterUsuario, 


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
