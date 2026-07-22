

using System.ComponentModel.DataAnnotations;

namespace AppDeMensagem.Feature.Auth.Models;

public sealed record LoginUserModel
{
    [Required(ErrorMessage = "O E-mail é obrigatorio para efetuar o login. ")]
    [EmailAddress(ErrorMessage = "O E-mail é invalído. ")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatoria para efetuar o login. ")]
    [MinLength(3, ErrorMessage = "A senha é muito curta. ")]
    public string Password { get; set; } = string.Empty;
}