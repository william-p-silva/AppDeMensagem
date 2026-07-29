

namespace AppDeMensagem.Application.DTOs.Chat.Response;

public sealed record ResponseChat
{
    public Guid Chat_ID { get; set; }
    public DateTime Created { get; set; }
    public bool Ativo { get; set; }
    public List<ResponseParticipantsInChat> Participants { get; set; }

}
