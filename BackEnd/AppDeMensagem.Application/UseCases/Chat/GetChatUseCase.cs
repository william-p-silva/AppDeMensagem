

using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.DTOs.Chat.Response;
using AppDeMensagem.Application.Interfaces.Repositorys;

namespace AppDeMensagem.Application.UseCases.Chat;

public class GetChatUseCase(IChatRepository chatRepository)
{
    public async Task<ResponseGetChat> ExecuteAsync(RequestGetChat request, Guid userId)
    {
        var chat = await chatRepository.GetByIdAsync(request.Chat_ID);

        if (chat is null)
            throw new ArgumentNullException("Chat not found. ");

        var currentUserChat = chat.UsersChat.FirstOrDefault(uc => uc.User_ID == userId);

        return new ResponseGetChat
        {
            Chat_ID = chat.Chat_ID,
            Ativo = chat.Ativo,
            Created = chat.Created,
            NameChat = currentUserChat?.NameChat ?? chat.Name,
            Participants = chat.UsersChat.Select(p => new ResponseParticipantsInChat
            {
                User_ID = p.User_ID,
                Name = p.Usuario.UserName.TextName,
                Email = p.Usuario.EmailAddress.Endereco,
                IsAdmin = p.IsAdmin
            }).ToList(),
            Messages = chat.Messages.Select(m => new ResponseMessageChat
            {
                Message_ID = m.Message_ID,
                Sender = new ResponseSenderMessage
                {
                    UserChat_ID = m.Sender_ID,
                    User_ID = m.Sender.User_ID,
                    Name = m.Sender.Usuario.UserName.TextName,
                    Email = m.Sender.Usuario.EmailAddress.Endereco
                },
                TextMessage = m.Text,
                StatusMessage = m.Status.ToString(),
                SendTime = m.SendTime
            }).ToList()
        };
    }
}
