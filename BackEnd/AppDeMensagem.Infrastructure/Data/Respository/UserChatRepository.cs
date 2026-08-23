
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Infrastructure.Data.Context;

namespace AppDeMensagem.Infrastructure.Data.Respository;

public class UserChatRepository(AppDbContext context) : IUserChatRepository
{
    public void Delete(UserChat userChat)
    {
        context.Remove(userChat);
    }
}
