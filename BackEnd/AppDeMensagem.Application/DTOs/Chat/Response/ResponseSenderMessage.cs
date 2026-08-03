
namespace AppDeMensagem.Application.DTOs.Chat.Response;

public sealed record ResponseSenderMessage
{
    public Guid UserChat_ID { get; set; }
    public Guid User_ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}