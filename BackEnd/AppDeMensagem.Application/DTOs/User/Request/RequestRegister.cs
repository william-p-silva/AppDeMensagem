using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Application.DTOs.User.Request;

public sealed record RequestRegister
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string PassWord { get; set; }
    public string ConfirmPassword { get; set; }
    public PerfilUser Profile { get; set; }
}
