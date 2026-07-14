
using AppDeMensagem.Domain.ValueObjects;

namespace AppDeMensagem.Domain.Entity;

public class Usuario
{
    public Email Email { get; private set; }
    public Name UserName { get; private set; }
    public string PasswordHash { get; private set; }
}
