
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

        chatRepository.TrackNewMessage(message);

        await unitOfWork.CommitAsync();

        var messageResponse = new ResponseMessage
        {
            Message_ID = message.Message_ID,
            TextMessage = message.Text,
            StatusMessage = message.Status.ToString(),
            SendTime = message.SendTime,
            Sender = new ResponseSenderMessage
            {
                Email = sender.Usuario.EmailAddress.Endereco,
                Name = sender.Usuario.UserName.TextName,
                UserChat_ID = sender.UserChat_ID,
                User_ID = sender.User_ID
            }
        };

        await chatNotificationService.NotifyMessageSentAsync(messageResponse, chatId: chat.Chat_ID);

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
