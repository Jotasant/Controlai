using Microsoft.Extensions.Configuration;
using Applicacao.Servicos;
using Applicacao.Interfaces;
using Dominio.Interfaces;
using Infraestrutura.Repositorio;
using Microsoft.OpenApi.Models;
using Infraestrutura.Data;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


builder.Services.AddScoped<IRepoUsuarioSistema, RepoUsuarioSistema>();
builder.Services.AddScoped<ISvcObterUsuario, SvcObterUsuario>();
builder.Services.AddScoped<ConexaoRepo>();
builder.Services.AddScoped<SvcCriarUsuario>();
builder.Services.AddScoped<SvcEditarUsuario>();
builder.Services.AddScoped<SvcExcluirUsuario>();
builder.Services.AddScoped<SvcAutenticarUsuario>();



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Controlai API", Version = "v1" });
});

builder.Configuration.AddJsonFile("ConnectString.json", optional: false, reloadOnChange: true);

builder.WebHost.ConfigureKestrel(serverOptions => {serverOptions.ListenAnyIP(7770);});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Controlai API V1");
        options.RoutePrefix = string.Empty; 
    });
}
else
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    Console.WriteLine($"Requisição: {context.Request.Path}");
    await next();
});

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization(); 

app.MapControllers();

app.Run();