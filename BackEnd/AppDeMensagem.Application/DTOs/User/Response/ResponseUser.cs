

using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Application.DTOs.User.Response;

public sealed record ResponseUser
{
    public string Profile { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid User_ID { get; set; }
}
