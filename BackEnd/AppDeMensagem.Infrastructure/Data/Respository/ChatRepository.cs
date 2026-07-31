

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
                && context.UsersChat.Any(uc => uc.User_ID == userId))
            .ToListAsync();
    }

    public async Task<List<ChatGroup>> GetByGroupAsync(Guid userId, bool? ativo = null)
    {
        return await context.ChatsGroup
            .AsNoTracking()
            .Include(c => c.UsersChat)
                .ThenInclude(uc => uc.Usuario)
            .Where(x => (ativo == null || x.Ativo == ativo)
                && context.UsersChat.Any(uc => uc.User_ID == userId))
            .ToListAsync();
    }

    public async Task<List<UserChat>> GetByGroupUserChatAsync(Guid userId, bool? ativo = null)
    {
        return await context.UsersChat
                .Include(x => x.Chat)
                .Include(x => x.Usuario)
                .Where(x => x.User_ID == userId
                         && x.Chat is ChatPrivate // <- Filtra apenas se o Chat for ChatPrivate
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
                 && context.UsersChat.Any(uc => uc.User_ID == userId))
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


    public void TrackNewMessage(Message message)
    {
        context.Messages.Add(message);
    }
}
