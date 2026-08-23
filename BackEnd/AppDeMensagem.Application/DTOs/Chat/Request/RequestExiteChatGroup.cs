

namespace AppDeMensagem.Application.DTOs.Chat.Request;

public sealed record RequestExiteChatGroup
{
    public Guid Chat_ID { get; set; }
}
