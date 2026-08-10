
using System.ComponentModel.DataAnnotations;

namespace AppDeMensagem.Feature.User.Model.Chat;

public sealed record RequestMesageModel
{
    public Guid Chat_ID { get; set; } = Guid.Empty;

    [Required(ErrorMessage = "A mensagen precisa de um texto")]
    public string TextMessage { get; set; } = string.Empty;
}
