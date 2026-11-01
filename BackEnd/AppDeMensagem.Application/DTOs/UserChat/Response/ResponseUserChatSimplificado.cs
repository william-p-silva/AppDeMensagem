

namespace AppDeMensagem.Application.DTOs.UserChat.Response;

public sealed record ResponseUserChatSimplificado
{
    public Guid UserChat_ID { get; set; }
    public Guid User_ID { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}
