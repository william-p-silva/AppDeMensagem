

using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.Interfaces.Repositorys;

public interface IUserChatRepository
{
    void Delete(UserChat userChat);
}
