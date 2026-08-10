

using AppDeMensagem.Core.Http;
using AppDeMensagem.Feature.User.Model.Chat;

namespace AppDeMensagem.Feature.User.Services.Chat;

public class ChatService
{
    private readonly ChatHubService _chatHubService;
    private readonly HttpService _httpService;

    public ChatService(ChatHubService chatHubService, HttpService httpService)
    {
        _chatHubService = chatHubService;
        _httpService = httpService;

        _chatHubService.OnMessageReceived += HandleMessageReceived;
    }

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
        if(ChatHistory.Chat_ID != Guid.Empty && ChatHistory.Chat_ID != chatId)
        {
            await _chatHubService.LeaveChatAsync(ChatHistory.Chat_ID);
        }


        ChatActive = true;
        await GetChatHistory(chatId);

        await _chatHubService.JoinChatAsync(ChatHistory.Chat_ID);
    }

    private async Task GetChatHistory(Guid chatId)
    {
        var response = await _httpService.GetAsync<ChatHistoryModel>($"Chat/get?Chat_ID={chatId}");
        if (response.Data is not null && _httpService.Error.Count == 0)
        {
            ChatHistory = response.Data;
            NotifyStateChanged();
        }
        else
        {
            ErrorChat.Add(_httpService.Error.Last());
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
        var response = await _httpService.PostAsync<RequestMesageModel, string>("Chat/post/send-message", RequestSendMessage);

        if (_httpService.Error.Count > 0)
            ErrorChat.Add(_httpService.Error.Last());


    }

    private void HandleMessageReceived(MessageChatModel message)
    {
        if (ChatHistory.Chat_ID == Guid.Empty)
            return;

        ChatHistory.Messages.Add(message);

        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();


}
