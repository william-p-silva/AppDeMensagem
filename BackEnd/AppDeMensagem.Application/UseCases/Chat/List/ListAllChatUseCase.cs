

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

        return chats.Select(x => new ResponseChat
        {
            Ativo = x.Ativo,
            Chat_ID = x.Chat_ID,
            Created = x.Created,
            Participants = x.UsersChat.Select(u => new ResponseParticipantsInChat
            {
                Email = u.Usuario.EmailAddress.Endereco,
                IsAdmin = u.IsAdmin,
                Name = u.Usuario.UserName.TextName,
                User_ID = u.User_ID
            }).ToList()
            
        }).ToList();
    }
}
