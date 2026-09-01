

namespace AppDeMensagem.Application.DTOs.User.Response;

public sealed record ResponseProfileUser
{
    public string Profile { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
