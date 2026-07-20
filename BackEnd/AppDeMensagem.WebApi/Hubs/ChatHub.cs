
using Microsoft.AspNetCore.SignalR;

namespace AppDeMensagem.WebApi.Hubs;

public class ChatHub : Hub
{
    public async Task JoinChatGroup(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
    public async Task LeaveChatGroup(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}
