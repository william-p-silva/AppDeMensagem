
namespace AppDeMensagem.Application.DTOs.Chat.Response;

public sealed record ResponseSendMessage
{
    public Guid Chat_ID { get; set; }
    public Guid User_Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public DateTime SendTime { get; set; }
    public string TextMessage { get; set; }
}
