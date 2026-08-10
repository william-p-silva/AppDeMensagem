

using AppDeMensagem.Application.DTOs.Chat.Response;

namespace AppDeMensagem.Application.Interfaces.Services;

public interface IChatNotificationService
{
    Task NotifyMessageSentAsync(ResponseMessage responseMessage, Guid chatId);
}
