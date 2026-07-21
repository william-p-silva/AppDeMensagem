using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Security;
using AppDeMensagem.Application.Interfaces.Services;
using AppDeMensagem.Application.UseCases.Chat.Create;
using AppDeMensagem.Application.UseCases.User;
using AppDeMensagem.Infrastructure.Data.Respository;
using AppDeMensagem.Infrastructure.Data.Security;
using AppDeMensagem.WebApi.Services.Chat;


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
        services.AddScoped<IChatNotificationService, SignalRChatNotificationService>();
        services.AddScoped<ITokenService, TokenService>();

        // UseCases
        // User
        services.AddScoped<RegisterUseCase>();
        services.AddScoped<LoginUseCase>();
        // Chat
        services.AddScoped<CreateChatPrivateUseCase>();


        return services;
    }
}
