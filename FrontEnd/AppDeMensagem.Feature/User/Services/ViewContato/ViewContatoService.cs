using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.User.Model.ViewContato;

namespace AppDeMensagem.Feature.User.Services.ViewContato;

public class ViewContatoService(HttpService httpService)
{
    public bool IsLoading { get; set; } = true;
    public List<string> ErrorChats { get; private set; } = new List<string>();
    public List<ChatsModel> Chats { get; private set; } = new List<ChatsModel>();

    private async Task<List<ChatsModel>> GetChats()
    {
        var response = await httpService.GetAsync<List<ChatsModel>>("Chat/get/all");

        if (!response.Success)
        {
            ErrorChats.Add(httpService.Error.Last());
            return new List<ChatsModel>();
        }
        if (ErrorChats.Count > 0)
            return new List<ChatsModel>();
        if (response.Data is null)
        {
            ErrorChats.Add("Erro na busca de conversas. Tente novamente. ");
            return new List<ChatsModel>();
        }

        return response.Data;
    }

    public async Task SetChats()
    {
        var chats = await GetChats();

        Chats = chats ?? new List<ChatsModel>();

        IsLoading = false;
    }
}
