
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IUserRepository
{
    Task<Usuario?> SearchById(Guid user_id);
    Task<List<Usuario>> ListAsync(PerfilUser? profile = PerfilUser.User);
    Task AddAsync(Usuario user);
    void Delete(Usuario user);
}
