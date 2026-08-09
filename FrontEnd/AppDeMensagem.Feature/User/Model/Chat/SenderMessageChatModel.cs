

namespace AppDeMensagem.Feature.User.Model.Chat;

public sealed record SenderMessageChatModel
{
    public Guid UserChat_ID { get; set; }
    public Guid User_ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
