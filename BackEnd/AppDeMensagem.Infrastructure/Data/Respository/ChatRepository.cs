

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

    public async Task<List<Chat>> GetAllAsync(bool? ativo)
    {
        if (ativo is null)
        {
            return await context.Chats
                .Include(x => x.UsersChat)
                .ThenInclude(x => x.Usuario)
                .ToListAsync();

        }
        return await context.Chats
                .Where(c => c.Ativo == ativo)
                .Include(x => x.UsersChat)
                .ThenInclude(x => x.Usuario)
                .ToListAsync();
    }

    public async Task<List<ChatGroup>> GetByGroupAsync(bool? ativo)
    {
        if (ativo is null)
        {
            return await context.ChatsGroup
                .Include(x => x.UsersChat)
                .ThenInclude(x => x.Usuario)
                .ToListAsync();

        }
        return await context.ChatsGroup
                .Where(c => c.Ativo == ativo)
                .Include(x => x.UsersChat)
                .ThenInclude(x => x.Usuario)
                .ToListAsync();
    }

    public async Task<List<ChatPrivate>> GetByPrivateAsync(bool? ativo)
    {
        if (ativo is null)
        {
            return await context.ChatsPrivate
                .Include(x => x.UsersChat)
                .ThenInclude(x => x.Usuario)
                .ToListAsync();

        }
        return await context.ChatsPrivate
            .Where(c => c.Ativo == ativo)
            .Include(x => x.UsersChat)
            .ThenInclude(x => x.Usuario)
            .ToListAsync();
    }

    public Task<Chat?> GetByIdWithParticipantsAsync(Guid chatId)
    {
        return context.Chats
            .Include(u => u.UsersChat)
            .ThenInclude(u => u.Usuario)
            .FirstOrDefaultAsync(c => c.Chat_ID == chatId);
    }
}
