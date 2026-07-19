
using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.DTOs.Chat.Response;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Services;

namespace AppDeMensagem.Application.UseCases.Chat;

public class SendMessageUseCase(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork,
    IChatNotificationService chatNotificationService
    )
{
    public async Task<ResponseSendMessage> ExecuteAsync(RequestSendMessage request, Guid userId)
    {
        var chat = await chatRepository.GetByIdWithParticipantsAsync(request.Chat_ID);
        if (chat is null)
            throw new ArgumentException("The chat not exist. ");

        var sender = chat.UsersChat.FirstOrDefault(u => u.User_ID == userId);
        if (sender is null)
            throw new UnauthorizedAccessException("The user not participate in this chat. ");

        chat.SendMessage(sender, request.TextMessage);

        var message = chat.Messages.Last();

        await unitOfWork.CommitAsync();

        await chatNotificationService.NotifyMessageSentAsync(chat.Chat_ID, userId, request.TextMessage);

        return new ResponseSendMessage
        {
            Chat_ID = chat.Chat_ID,
            SendTime = message.SendTime,
            UserEmail = sender.Usuario.EmailAddress.Endereco,
            UserName = sender.Usuario.UserName.TextName,
            User_Id = sender.User_ID,
            TextMessage = message.Text,
        };
    }
}
