
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
        try
        {
            var response = await _httpService.PostAsync<CadastroUserModel, string>("User/post", CadastroModel);

            return response.Success;
        }
        catch (HttpError ex)
        {
            ErrorCadastro.Add(ex.Message);
            return false;
        }
    }

}
