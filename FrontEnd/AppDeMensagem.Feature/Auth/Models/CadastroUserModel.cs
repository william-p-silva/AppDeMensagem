

using System.ComponentModel.DataAnnotations;

namespace AppDeMensagem.Feature.Auth.Models;

public sealed record CadastroUserModel
{
    [Required(ErrorMessage = "O E-mail é obrigatorio para efetuar o login. ")]
    [EmailAddress(ErrorMessage = "O E-mail é invalído. ")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nome é obrigatorio no cadastro. ")]
    [MinLength(6, ErrorMessage = "O nome é muito curto, coloque seu nome completo. ")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatoria no cadastro. ")]
    [MinLength(3, ErrorMessage = "A senha é muito curta. ")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "A confirmação de senha é obrigatoria no cadastro. ")]
    [MinLength(3, ErrorMessage = "A senha é muito curta. ")]
    [Compare(nameof(Password), ErrorMessage = "Ops! A confirmação está diferente da senha informada.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "É necessario um perfil para o usuário")]
    public ProfileUser Profile { get; set; } = ProfileUser.User;
}
