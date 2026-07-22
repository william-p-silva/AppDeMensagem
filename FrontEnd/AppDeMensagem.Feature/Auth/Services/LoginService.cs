

using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.Auth.Models;

namespace AppDeMensagem.Feature.Auth.Services;

public class LoginService(HttpService httpService)
{
    private readonly HttpService _httpService = httpService;
    public List<string> ErrorLogin { get; set; } = new List<string>();

    public async Task<bool> LogarUser(LoginUserModel request)
    {        
        var response = await _httpService.PostAsync<LoginUserModel, ResponseLoginUserModel>("User/post/login", request);
        if (_httpService.Error.Count > 0)
        {
            ErrorLogin.Add(_httpService.Error.Last());
        }
        return response?.Success ?? false;
    }
}
