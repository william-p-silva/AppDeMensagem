
using AppDeMensagem.Application.Interfaces.Services;

namespace AppDeMensagem.Infrastructure.Data.temp;

public class Temp : IChatNotificationService
{
    public Task NotifyMessageSentAsync(Guid chatId, Guid senderId, string text)
    {
        throw new NotImplementedException();
    }
}
