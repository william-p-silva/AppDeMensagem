
using AppDeMensagem.Core.Http;
using AppDeMensagem.Core.Model.Enum.User;
using AppDeMensagem.Core.Model.User;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AppDeMensagem.Core.Configuration;

public class Authentication
{
    private readonly HttpService? _http;

    public ProfileUser Profile { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public bool IsAuthenticated { get; private set; }

    public Authentication(HttpService? http)
    {
        _http = http;
    }

    public Authentication(string email, string name, string profile)
    {
        ValidateData(email, name, profile);
        IsAuthenticated = true;
    }

    public static async Task<Authentication> FindUerByTokenAsync(HttpService http)
    {
        var auth = new Authentication(http);
        await auth.VerifyTokenAsync();
        return auth;
    }

    public async Task<bool> VerifyTokenAsync()
    {
        if (_http == null)
            throw new InvalidOperationException("HttpService não foi fornecido para validação.");

        var user = await FindUser();

        if (user != null)
        {
            ValidateData(user.Email, user.Name, user.Profile);
            IsAuthenticated = true;
            return true;
        }

        IsAuthenticated = false;
        return false;
    }

    private void ValidateData(string email, string name, string profile)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidOperationException("The email cannot be null.");
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("The name cannot be null.");

        if (string.IsNullOrWhiteSpace(profile))
            throw new InvalidOperationException("The profile cannot be null.");
        if (Enum.TryParse<ProfileUser>(profile, true, out ProfileUser profileUser))
        {
            Profile = profileUser;
        }
        else
        {
            throw new InvalidOperationException("The profile not match. ");
        }

        var validatorEmail = new EmailAddressAttribute();
        if (!validatorEmail.IsValid(email))
            throw new InvalidOperationException("The email not is valid. ");

        Name = name;
        Email = email;
    }

    private async Task<UserModel?> FindUser()
    {
        if (_http is null) return null;

        var user = await _http.PostAsync<string, UserModel> ("User/me", request: null);
        if (user.Data is null || !user.Success)
            return null;
        
        return user.Data;
    }

    public ClaimsPrincipal ToClaimsPrincipal()
    {
        if (!IsAuthenticated)
            return new ClaimsPrincipal(new ClaimsIdentity()); // Usuário Anônimo

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Name),
            new Claim(ClaimTypes.Email, Email),
            new Claim(ClaimTypes.Role, Profile.ToString())
        };

        var identity = new ClaimsIdentity(claims, "ApiAuth");
        return new ClaimsPrincipal(identity);
    } 
}
