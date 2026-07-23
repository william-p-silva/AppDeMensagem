
using AppDeMensagem.Core.Http;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AppDeMensagem.Core.Configuration;

public class CustomAuthStateProvider(HttpService http) : AuthenticationStateProvider
{
    private readonly HttpService _http = http;
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            Authentication auth = await Authentication.FindUerByTokenAsync(_http);

            if (auth.IsAuthenticated)
            {
                return new AuthenticationState(auth.ToClaimsPrincipal());
            }
        }
        catch
        {        
        }

        var anonymousIdentity = new ClaimsIdentity();
        return new AuthenticationState(new ClaimsPrincipal(anonymousIdentity));
    }

    /// <summary>
    /// Chame este método após o Login ou Logout para atualizar toda a interface instantaneamente.
    /// </summary>
    public void NotifyUserAuthenticationState()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
