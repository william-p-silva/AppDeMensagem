

using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IMessageRepository
{
    Task<List<Message>> GetByIDsToListAsync(List<Guid> message_ids);
}
