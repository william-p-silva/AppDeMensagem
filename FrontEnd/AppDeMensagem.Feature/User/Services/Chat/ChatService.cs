

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

    public RequestMesageModel RequestSendMessage { get; set; } =
        new RequestMesageModel()
        {
            Chat_ID = Guid.Empty,
            TextMessage = string.Empty
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

    public async Task SendMessage()
    {
        if (string.IsNullOrWhiteSpace(RequestSendMessage.TextMessage))
        {
            ErrorChat.Add("O texto da mensagem não pode ser vazio.");
        }

        RequestSendMessage.Chat_ID = ChatHistory.Chat_ID;

        await SendMessageService();

        RequestSendMessage = new RequestMesageModel()
        {
            Chat_ID = Guid.Empty,
            TextMessage = string.Empty
        };
    }

    private async Task SendMessageService()
    {
        var response = await httpService.PostAsync<RequestMesageModel, string>("Chat/post/send-message", RequestSendMessage);

        if (httpService.Error.Count > 0)
            ErrorChat.Add(httpService.Error.Last());


    }

    private void NotifyStateChanged() => OnChange?.Invoke();


}
