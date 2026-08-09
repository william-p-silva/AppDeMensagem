

namespace AppDeMensagem.Feature.User.Model.Chat;

public sealed record ChatHistoryModel
{
    public Guid Chat_ID { get; set; }
    public bool Ativo { get; set; }
    public DateTime Created { get; set; }
    public string NameChat { get; set; }
    public List<ParticipantsInChatModel> Participants { get; set; }
    public List<MessageChatModel> Messages { get; set; }
}
