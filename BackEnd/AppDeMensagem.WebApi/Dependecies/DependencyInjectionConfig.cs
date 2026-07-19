using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Security;
using AppDeMensagem.Application.Interfaces.Services;
using AppDeMensagem.Application.UseCases.User;
using AppDeMensagem.Infrastructure.Data.Respository;
using AppDeMensagem.Infrastructure.Data.Security;
using AppDeMensagem.Infrastructure.Data.temp;


namespace AppDeMensagem.WebApi.Dependecies;

internal static class DependencyInjectionConfig
{
    public static IServiceCollection AddProjectDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IChatNotificationService, Temp>();

        // UseCases
        // User
        services.AddScoped<RegisterUseCase>();

        return services;
    }
}
