using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Infrastructure.Data.Context;
using AppDeMensagem.WebApi.Dependecies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// ============================================================================
// 1. CONFIGURAÇÃO DE INFRAESTRUTURA DE DADOS (SQL Server 2025 Developer)
// ============================================================================
var conecctionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(conecctionString, x => x.MigrationsAssembly("AppDeMensagem.Infrastructure")));

builder.Services.AddProjectDependecies(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Criar uma nova Migration:
// dotnet ef migrations add init --project ..\AppDeMensagem.Infrastructure 
//
// Atualizar o banco manualmente (Caso não queira depender do auto-migrate):
// dotnet ef database update --project ..\AppDeMensagem.Infrastructure  
