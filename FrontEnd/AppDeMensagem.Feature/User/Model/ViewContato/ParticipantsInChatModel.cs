

namespace AppDeMensagem.Feature.User.Model.ViewContato;

public sealed record ParticipantsInChatModel
{
    public Guid User_ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
}
