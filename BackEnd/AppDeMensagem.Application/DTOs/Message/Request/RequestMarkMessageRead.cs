
namespace AppDeMensagem.Application.DTOs.Message.Request;

public sealed record RequestMarkMessageRead
{
    public List<Guid> Message_IDs { get; set; }
    public Guid Reader_ID { get; set; }
}
