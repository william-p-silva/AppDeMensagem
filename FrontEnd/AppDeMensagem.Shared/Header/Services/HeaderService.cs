
using AppDeMensagem.Core.Configuration;
using AppDeMensagem.Core.Services;
using AppDeMensagem.Shared.Header.Models;

namespace AppDeMensagem.Shared.Header.Services;

public class HeaderService(
    UserSessionService userSessionService,
    Authentication authentication
    )
{
    public List<LinksHeader> AuthLinks { get; set; } = 
        new List<LinksHeader>()
        {
            new LinksHeader("/login", "Login"),
            new LinksHeader("/cadastro", "Cadastro"),
        };

    public bool IsLoading { get; private set; } = true;
    public bool IsLogged { get; private set; } = false;
    public string? Profile { get; private set; }
    public string? Email { get; private set; }
    public string? Name { get; private set; }
    public bool IsAdmin { get; private set; } = false;
    public bool IsUser { get; private set; } = false;

    public async Task GetUser()
    {
        IsLogged = await userSessionService.IsAuthenticatedAsync();
        if (IsLogged)
        {
            Name = await userSessionService.GetNameUserAsync();
            Email = await userSessionService.GetEmailUserAsync();
            Profile = await userSessionService.GetProfileUserAsync();
            IsAdmin = await userSessionService.IsInRoleAsync("Admin");
            IsUser = await userSessionService.IsInRoleAsync("User");
        }
        IsLoading = false;
    }

    public async Task<bool> Logout()
    {
        var response = await authentication.Logout();

        return response;
    }
}
