

using AppDeMensagem.Core.Configuration;
using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.Auth.Models;

namespace AppDeMensagem.Feature.Auth.Services;

public class LoginService(HttpService httpService)
{
    private readonly HttpService _httpService = httpService;

    public LoginUserModel ModelUser { get; set; } = new();
    public List<string> ErrorLogin { get; set; } = new List<string>();

    public async Task<bool> LogarUser()
    {        
        var response = await _httpService.PostAsync<LoginUserModel, ResponseLoginUserModel>("User/post/login", ModelUser);

        if (_httpService.Error.Count > 0)
        {
            ErrorLogin.Add(_httpService.Error.Last());
        }
        if (response.Data is null)
        {
            ErrorLogin.Add("Ocorreu um erro no login. ");
            return false;
        }

        Authentication auth = new Authentication(
            email: response.Data.Email, name: response.Data.Name, profile: response.Data.Profile);

        return response?.Success ?? false;
    }
}
