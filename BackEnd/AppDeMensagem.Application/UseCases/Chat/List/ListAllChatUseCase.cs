

using AppDeMensagem.Application.DTOs.Chat.Response;
using AppDeMensagem.Application.Interfaces.Repositorys;

namespace AppDeMensagem.Application.UseCases.Chat.List;

public class ListAllChatUseCase(
    IChatRepository chatRepository
    )
{

    public async Task<List<ResponseChat>> ExecuteAsync(Guid userId)
    {
        var chats = await chatRepository.GetAllAsync(userId);

        return chats.Select(chat =>
        {
            // Busca a relação do usuário logado neste chat específico
            var currentUserChat = chat.UsersChat.FirstOrDefault(uc => uc.User_ID == userId);

            return new ResponseChat
            {
                Chat_ID = chat.Chat_ID,
                Created = chat.Created,
                Ativo = chat.Ativo,
                // Pega o NameChat customizado do usuário ou usa o nome padrão do Chat se for nulo
                Name = currentUserChat?.NameChat ?? chat.Name,
                Participants = chat.UsersChat.Select(u => new ResponseParticipantsInChat
                {
                    User_ID = u.User_ID,
                    Name = u.Usuario.UserName.TextName,
                    Email = u.Usuario.EmailAddress.Endereco,
                    IsAdmin = u.IsAdmin
                }).ToList()
            };
        }).ToList();
    }
}
