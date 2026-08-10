

namespace AppDeMensagem.Application.DTOs.Chat.Response;

public sealed record ResponseMessage
{
    public Guid Message_ID { get; set; }
    public string TextMessage { get; set; }
    public string StatusMessage { get; set; }
    public DateTime SendTime { get; set; }
    public ResponseSenderMessage Sender { get; set; }
}
