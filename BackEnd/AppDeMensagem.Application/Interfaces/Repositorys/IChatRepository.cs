
using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IChatRepository
{
    Task<Chat?> GetByIdWithParticipantsAsync(Guid chatId);
}
