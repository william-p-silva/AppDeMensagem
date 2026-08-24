

using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IUserChatRepository
{
    Task<List<UserChat>> GetAllAsync();
    void Delete(UserChat userChat);
}
