

namespace AppDeMensagem.Application.DTOs.User.Response;

public sealed record ResponseLogin
{
    public string Token { get; set; }
    public string Profile { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
