

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDeMensagem.Infrastructure.Data.Respository;

public class ChatRepository(AppDbContext context) : IChatRepository
{
    public async Task AddAsync(Chat chat)
    {
        await context.Chats.AddAsync(chat);
    }

    public async Task<List<Chat>> GetAllAsync(Guid userId, bool? ativo = null)
    {
        return await context.Chats
            .AsNoTracking()
            .Include(c => c.UsersChat)
                .ThenInclude(uc => uc.Usuario)
            .Where(x => (ativo == null || x.Ativo == ativo)
            && x.UsersChat.Any(uc => uc.User_ID == userId))
            .ToListAsync();
    }

    public async Task<List<ChatGroup>> GetByGroupAsync(Guid userId, bool? ativo = null)
    {
        return await context.ChatsGroup
            .AsNoTracking()
            .Include(c => c.UsersChat)
                .ThenInclude(uc => uc.Usuario)
            .Where(x => (ativo == null || x.Ativo == ativo)
                && x.UsersChat.Any(uc => uc.User_ID == userId))
            .ToListAsync();
    }

    public async Task<List<UserChat>> GetByGroupUserChatAsync(Guid userId, bool? ativo = null)
    {
        return await context.UsersChat
                .Include(x => x.Chat)
                .Include(x => x.Usuario)
                .Where(x => x.User_ID == userId
                         && x.Chat is ChatGroup
                         && (ativo == null || x.Chat.Ativo == ativo))
                .ToListAsync();
    }

    public async Task<List<ChatPrivate>> GetByPrivateAsync(Guid userId, bool? ativo = null)
    {
        return await context.ChatsPrivate
             .AsNoTracking()
             .Include(c => c.UsersChat)
                 .ThenInclude(uc => uc.Usuario)
             .Where(x => (ativo == null || x.Ativo == ativo)
                 && x.UsersChat.Any(uc => uc.User_ID == userId))
             .ToListAsync();
    }

    public async Task<Chat?> GetByIdWithParticipantsAsync(Guid chatId)
    {
        return await context.Chats
                .AsTracking() // Garante que a busca rastreará explicitamente o tipo concreto
                .Include(u => u.UsersChat)
                    .ThenInclude(u => u.Usuario)
                .Include(m => m.Messages)
                .FirstOrDefaultAsync(c => c.Chat_ID == chatId);
    }

    public async Task<Chat?> GetByIdAsync(Guid chatId)
    {
        return await context.Chats
                .AsTracking() // Garante que a busca rastreará explicitamente o tipo concreto
                .AsSplitQuery()
                .Include(u => u.UsersChat)
                    .ThenInclude(u => u.Usuario)
                .Include(m => m.Messages.OrderBy(x => x.SendTime))
                    .ThenInclude(m => m.Sender)
                        .ThenInclude(s => s.Usuario)
                .FirstOrDefaultAsync(c => c.Chat_ID == chatId);
    }

    public void TrackNewMessage(Message message)
    {
        context.Messages.Add(message);
    }

    public async Task<ChatGroup?> FindChatGroupByIdAsync(Guid chat_id, Guid user_id, bool? ativo = null)
    {
        return await context.ChatsGroup
            .AsTracking()
            .AsSplitQuery()
            .Include(c => c.UsersChat)
            .FirstOrDefaultAsync(
                    c => c.Chat_ID == chat_id
                    && (ativo == null || c.Ativo == ativo)
                    && c.UsersChat.Any(u => u.User_ID == user_id)
                    );
    }

    public async Task<UserChat?> FindPrivateChatBetweenUsersAsync(
        Guid primaryUserId, 
        Guid secondUserId)
    {
        return await context.UsersChat
            .AsTracking()
            .IgnoreQueryFilters()
            .Include(uc => uc.Chat)
            .Include(uc => uc.Usuario)
            .Where(uc =>
                uc.User_ID == primaryUserId &&
                uc.Chat is ChatPrivate &&
                uc.Chat.UsersChat.Any(other => other.User_ID == secondUserId))
            .FirstOrDefaultAsync();
    }

    public async Task<UserChat?> FindByIdWithUserId(Guid chatId, Guid userId)
    {
        return await context.UsersChat
            .AsTracking()
            .Include(uc => uc.Chat)
            .Include(uc => uc.Usuario)
            .FirstOrDefaultAsync(uc => uc.Chat_ID == chatId && uc.User_ID == userId);
    }
}