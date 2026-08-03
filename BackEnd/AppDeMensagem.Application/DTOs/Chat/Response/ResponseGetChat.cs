

namespace AppDeMensagem.Application.DTOs.Chat.Response;

public sealed record ResponseGetChat
{
    public Guid Chat_ID { get; set; }
    public bool Ativo { get; set; }
    public DateTime Created { get; set; }
    public string NameChat { get; set; }
    public List<ResponseParticipantsInChat> Participants { get; set; }
    public List<ResponseMessageChat> Messages { get; set; }
}
