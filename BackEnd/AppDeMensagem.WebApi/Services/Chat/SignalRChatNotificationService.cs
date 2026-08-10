using AppDeMensagem.Application.DTOs.Chat.Response;
using AppDeMensagem.Application.Interfaces.Services;
using AppDeMensagem.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AppDeMensagem.WebApi.Services.Chat;

public class SignalRChatNotificationService(IHubContext<ChatHub> hubContext) : IChatNotificationService
{
    public async Task NotifyMessageSentAsync(ResponseMessage responseMessage, Guid chatId)
    {
        await hubContext.Clients.Group(chatId.ToString())
            .SendAsync("ReceiveMessage", responseMessage);
    }
}
