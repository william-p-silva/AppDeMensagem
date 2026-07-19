

namespace AppDeMensagem.Application.DTOs.Chat.Request;

public sealed record RequestSendMessage
{
    public Guid Chat_ID { get; set; }
    public string TextMessage { get; set; }
}
