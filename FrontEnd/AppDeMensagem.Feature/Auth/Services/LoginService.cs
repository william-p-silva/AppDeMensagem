

using AppDeMensagem.Core.Configuration;
using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.Auth.Models;
using Microsoft.AspNetCore.Components;

namespace AppDeMensagem.Feature.Auth.Services;

public class LoginService(HttpService httpService, NavigationManager navigation)
{
    private readonly HttpService _httpService = httpService;

    public LoginUserModel ModelUser { get; set; } = new();
    public List<string> ErrorLogin { get; set; } = new List<string>();

    public async Task<bool> LogarUser()
    {
        try
        {
            var response = await _httpService.PostAsync<LoginUserModel, ResponseLoginUserModel>("User/post/login", ModelUser);

            if (response.Data == null)
            {
                ErrorLogin.Add("Erro ao logar usuário");
                return false;
            }

            Authentication auth = new Authentication(
                email: response.Data.Email, name: response.Data.Name, profile: response.Data.Profile);

            navigation.NavigateTo("user/home");

            return response?.Success ?? false;
        }catch(HttpError ex)
        {
            ErrorLogin.Add(ex.Message);
            return false;
        }
    }
}
