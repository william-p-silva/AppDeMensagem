
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDeMensagem.Infrastructure.Data.Respository;

public class UserChatRepository(AppDbContext context) : IUserChatRepository
{
    public void Delete(UserChat userChat)
    {
        context.UsersChat.Remove(userChat);
    }

    public async Task<List<UserChat>> GetAllAsync()
    {
        return await context.UsersChat.Where(c => !c.Ativo).ToListAsync();
    }
}
