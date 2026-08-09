
namespace AppDeMensagem.Feature.User.Model.Chat;

public sealed record MessageChatModel
{
    public Guid Message_ID { get; set; }
    public string TextMessage { get; set; }
    public string StatusMessage { get; set; }
    public DateTime SendTime { get; set; }
    public SenderMessageChatModel Sender { get; set; }
}