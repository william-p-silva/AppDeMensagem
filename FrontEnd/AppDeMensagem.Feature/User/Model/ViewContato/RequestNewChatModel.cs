
using System.ComponentModel.DataAnnotations;

namespace AppDeMensagem.Feature.User.Model.ViewContato;

public class RequestNewChatModel
{
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    [MaxLength(250, ErrorMessage = "O campo Email deve ter no máximo 250 caracteres.")]
    public string Email { get; set; }
}
