

namespace AppDeMensagem.Feature.Admin.Models.User;

public sealed record UserModel
{
    public string Profile { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid User_ID { get; set; }
}
