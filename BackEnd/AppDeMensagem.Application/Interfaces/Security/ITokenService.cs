

using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Security;

public interface ITokenService
{
    string GenereteToken(Usuario user);
}
