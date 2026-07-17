using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Security;
using AppDeMensagem.Application.UseCases.User;
using AppDeMensagem.Infrastructure.Data.Respository;
using AppDeMensagem.Infrastructure.Data.Security;


namespace AppDeMensagem.WebApi.Dependecies;

internal static class DependencyInjectionConfig
{
    public static IServiceCollection AddProjectDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // UseCases
        // User
        services.AddScoped<RegisterUseCase>();

        return services;
    }
}
