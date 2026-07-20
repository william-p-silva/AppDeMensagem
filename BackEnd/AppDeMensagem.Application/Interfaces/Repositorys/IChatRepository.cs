
using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IChatRepository
{
    Task AddAsync(Chat chat);
    Task<Chat?> GetByIdWithParticipantsAsync(Guid chatId);
}
