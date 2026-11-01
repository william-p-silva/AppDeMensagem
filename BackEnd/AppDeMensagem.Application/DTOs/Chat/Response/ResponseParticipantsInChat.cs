

namespace AppDeMensagem.Application.DTOs.Chat.Response;

public sealed record ResponseParticipantsInChat
{
    public Guid User_ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}
