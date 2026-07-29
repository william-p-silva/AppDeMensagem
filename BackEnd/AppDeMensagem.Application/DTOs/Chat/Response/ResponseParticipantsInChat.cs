

namespace AppDeMensagem.Application.DTOs.Chat.Response;

public sealed record ResponseParticipantsInChat
{
    public Guid User_ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; } = false;
}
