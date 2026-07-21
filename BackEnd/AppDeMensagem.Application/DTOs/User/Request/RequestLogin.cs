

namespace AppDeMensagem.Application.DTOs.User.Request;

public sealed record RequestLogin
{
    public string Password { get; set; }
    public string Email { get; set; }
}

