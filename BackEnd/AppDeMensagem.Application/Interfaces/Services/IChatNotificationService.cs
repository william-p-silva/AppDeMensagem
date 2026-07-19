

namespace AppDeMensagem.Application.Interfaces.Services;

public interface IChatNotificationService
{
    Task NotifyMessageSentAsync(Guid chatId, Guid senderId, string text);
}
