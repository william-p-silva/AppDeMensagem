

namespace AppDeMensagem.Feature.Auth.Models;

public sealed record ResponseLoginUserModel
{
    public string Token { get; set; }
    public string Profile { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
