
namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IUnitOfWork
{
    Task CommitAsync();
}
