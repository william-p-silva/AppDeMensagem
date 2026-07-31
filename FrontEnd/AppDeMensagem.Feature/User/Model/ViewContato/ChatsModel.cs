
namespace AppDeMensagem.Feature.User.Model.ViewContato;

public sealed record ChatsModel
{
    public Guid Chat_ID { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public bool Ativo { get; set; }
    public List<ParticipantsInChatModel> Participants { get; set; }
}
