

using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.User.Model.Chat;

namespace AppDeMensagem.Feature.User.Services.Chat;

public class ChatService(HttpService httpService)
{
    public event Action? OnChange;

    public List<string> ErrorChat { get; private set; } = new List<string>();
    public bool ChatActive { get; private set; } = false;
    public ChatHistoryModel ChatHistory { get; private set; } =
        new ChatHistoryModel()
        {
            Chat_ID = Guid.Empty,
            Ativo = false,
            Created = DateTime.MinValue,
            NameChat = string.Empty,
            Participants = new List<ParticipantsInChatModel>(),
            Messages = new List<MessageChatModel>()
        };


    public async Task SetChatActive(Guid chatId)
    {
        ChatActive = true;
        await GetChatHistory(chatId);
    }

    private async Task GetChatHistory(Guid chatId)
    {
        var response = await httpService.GetAsync<ChatHistoryModel>($"Chat/get?Chat_ID={chatId}");
        if (response.Data is not null && httpService.Error.Count == 0)
        {
            ChatHistory = response.Data;
            NotifyStateChanged();
        }
        else
        {
            ErrorChat.Add(httpService.Error.Last());
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
