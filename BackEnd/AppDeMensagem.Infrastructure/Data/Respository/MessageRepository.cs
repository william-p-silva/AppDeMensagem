

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDeMensagem.Infrastructure.Data.Respository;

public class MessageRepository(AppDbContext context) : IMessageRepository
{
    public async Task<List<Message>> GetByIDsToListAsync(List<Guid> message_ids)
    {
        return await context.Messages.Where(m => message_ids.Contains(m.Message_ID)).ToListAsync();
    }

}
