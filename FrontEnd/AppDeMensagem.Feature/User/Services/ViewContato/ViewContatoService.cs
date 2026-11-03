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
        try
        {
            var response = await httpService.GetAsync<List<ChatsModel>>("Chat/get/all");

            return response.Data ?? new List<ChatsModel>();
        }
        catch (Exception ex)
        {
            ErrorChats.Add(ex.Message);
            return new List<ChatsModel>();
        }
        
    }

    public async Task SetChats()
    {
        var chats = await GetChats();

        Chats = chats ?? new List<ChatsModel>();

        IsLoading = false;
    }
}
