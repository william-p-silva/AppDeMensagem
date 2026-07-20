using AppDeMensagem.Application.Interfaces.Services;
using AppDeMensagem.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AppDeMensagem.WebApi.Services.Chat;

public class SignalRChatNotificationService(IHubContext<ChatHub> hubContext) : IChatNotificationService
{
    public async Task NotifyMessageSentAsync(Guid chatId, Guid senderId, string text)
    {
        await hubContext.Clients.Group(chatId.ToString())
            .SendAsync("ReceiveMessage", new
            {
                ChatId = chatId,
                SenderId = senderId,
                Text = text,
                SentAt = DateTime.UtcNow
            });
    }
}
