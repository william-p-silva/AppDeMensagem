

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

    public Task<Chat?> GetByIdWithParticipantsAsync(Guid chatId)
    {
        return context.Chats
            .Include(u => u.UsersChat)
            .ThenInclude(u => u.Usuario)
            .FirstOrDefaultAsync(c => c.Chat_ID == chatId);
    }
}
