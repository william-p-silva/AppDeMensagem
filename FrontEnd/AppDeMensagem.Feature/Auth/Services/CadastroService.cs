
using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.Auth.Models;

namespace AppDeMensagem.Feature.Auth.Services;

public class CadastroService(HttpService httpService)
{
    private readonly HttpService _httpService = httpService;

    public CadastroUserModel CadastroModel { get; set; } = new();
    public List<string> ErrorCadastro { get; set; } = new List<string>();

    public async Task<bool> RegisterUser()
    {
        var response = await _httpService.PostAsync<CadastroUserModel, string>("User/post", CadastroModel);

        if (_httpService.Error.Count > 0)
        {
            ErrorCadastro.Add(_httpService.Error.Last());
        }

        return response?.Success ?? false;
    }

}
