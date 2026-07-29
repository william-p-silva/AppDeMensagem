
using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IChatRepository
{
    Task AddAsync(Chat chat);
    Task<Chat?> GetByIdWithParticipantsAsync(Guid chatId);
    Task<List<Chat>> GetAllAsync(bool ativo);
    Task<List<ChatPrivate>> GetByPrivateAsync(bool ativo);
    Task<List<ChatGroup>> GetByGroupAsync(bool ativo);
 }
