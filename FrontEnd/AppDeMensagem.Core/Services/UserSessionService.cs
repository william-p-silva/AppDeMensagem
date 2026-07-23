

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AppDeMensagem.Core.Services;

public class UserSessionService(AuthenticationStateProvider authStateProvider)
{
    private async Task<ClaimsPrincipal> GetUserAsync()
    {
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var user = await GetUserAsync();
        return user.Identity?.IsAuthenticated ?? false;
    }

    public async Task<string?> GetEmailUserAsync()
    {
        var user = await GetUserAsync();

        return user.FindFirst(ClaimTypes.Email)?.Value;       
    }

    public async Task<string?> GetNameUserAsync()
    {
        var user = await GetUserAsync();

        return user.FindFirst(ClaimTypes.Name)?.Value;
    }

    public async Task<string?> GetProfileUserAsync()
    {
        var user = await GetUserAsync();

        return user.FindFirst(ClaimTypes.Role)?.Value;
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var user = await GetUserAsync();
        return user.IsInRole(role);
    }
}
