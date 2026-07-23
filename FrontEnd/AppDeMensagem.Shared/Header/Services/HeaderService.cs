
using AppDeMensagem.Core.Services;
using AppDeMensagem.Shared.Header.Models;

namespace AppDeMensagem.Shared.Header.Services;

public class HeaderService(UserSessionService userSessionService)
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

    public async Task GetUser()
    {
        IsLogged = await userSessionService.IsAuthenticatedAsync();
        if (IsLogged)
        {
            Name = await userSessionService.GetNameUserAsync();
            Email = await userSessionService.GetEmailUserAsync();
            Profile = await userSessionService.GetProfileUserAsync();
        }
        IsLoading = false;
    }
}
