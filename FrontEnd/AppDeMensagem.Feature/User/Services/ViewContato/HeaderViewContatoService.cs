using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.User.Model.ViewContato;

namespace AppDeMensagem.Feature.User.Services.ViewContato;

public class HeaderViewContatoService(HttpService httpService)
{
    // Evento para notificar os componentes sobre mudanças de estado
    public event Action? OnChange;

    public bool IsLoading { get; private set; } = false;
    public bool OpenModal { get; private set; } = false;
    public string HiddenModalClass { get; private set; } = "hidden";
    public List<string> ErrorHeader { get; private set; } = new List<string>();
    public RequestNewChatModel RequestChat { get; set; } = new RequestNewChatModel();

    private async Task PostNewChat()
    {
        var response = await httpService.PostAsync<RequestNewChatModel, string>
            ("Chat/post/private", RequestChat);
        if (response is null || !response.Success)
        {
            ErrorHeader.Add("Erro ao criar chat");
        }
        if (httpService.Error?.Count > 0)
        {
            ErrorHeader.Add(httpService.Error.Last());
        }
        if(response?.Data is null)
        {
            ErrorHeader.Add("Nenhum dado retornado");
        }
    }

    public async Task CreateNewChat()
    {
        IsLoading = true;
        ErrorHeader.Clear();
        await PostNewChat();
        IsLoading = false;
        CloseModalCreateChatPrivate();
        NotifyStateChanged();
    }

    public void OpenModalCreateChatPrivate()
    {
        OpenModal = true;
        HiddenModalClass = "flex";
        NotifyStateChanged();
    }

    public void CloseModalCreateChatPrivate()
    {
        OpenModal = false;
        ErrorHeader.Clear();
        HiddenModalClass = "hidden";
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
