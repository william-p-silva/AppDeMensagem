

namespace AppDeMensagem.Application.DTOs.Chat.Request;

public sealed record RequestGetChat
{
    public Guid Chat_ID { get; set; }
}
