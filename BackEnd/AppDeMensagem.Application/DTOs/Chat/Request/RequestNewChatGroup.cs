

namespace AppDeMensagem.Application.DTOs.Chat.Request;

public sealed record RequestNewChatGroup
{
    public List<Guid> Users_IDs { get; set; }
    public string Name { get; set; }
}
