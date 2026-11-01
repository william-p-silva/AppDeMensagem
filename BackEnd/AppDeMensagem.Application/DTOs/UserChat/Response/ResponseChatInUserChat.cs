

namespace AppDeMensagem.Application.DTOs.UserChat.Response;

public sealed record ResponseChatInUserChat
{
    public Guid Chat_ID { get; set; }
    public DateTime Created { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public List<ResponseUserChatSimplificado> UsersChat { get; set; } = new List<ResponseUserChatSimplificado>();
}