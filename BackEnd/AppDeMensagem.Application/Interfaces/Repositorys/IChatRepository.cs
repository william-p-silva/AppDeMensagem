
using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IChatRepository
{
    Task AddAsync(Chat chat);
    Task<Chat?> GetByIdWithParticipantsAsync(Guid chatId);
    Task<List<Chat>> GetAllAsync(Guid userId, bool? ativo = null);
    Task<List<ChatPrivate>> GetByPrivateAsync(Guid userId, bool? ativo = null);
    Task<Chat?> GetByIdAsync(Guid chatId);
    Task<List<ChatGroup>> GetByGroupAsync(Guid userId, bool? ativo = null);
    Task<ChatGroup?> FindChatGroupByIdAsync(
        Guid chat_id, Guid user_id, bool? ativo = null);
    Task<UserChat?> FindPrivateChatBetweenUsersAsync(
        Guid primaryUserId,
        Guid secondUserId );
    void TrackNewMessage(Message message);
    Task<UserChat?> FindByIdWithUserId(Guid chatId, Guid userId);


    Task<List<UserChat>> GetByGroupUserChatAsync(Guid userId, bool? ativo = null);
}
