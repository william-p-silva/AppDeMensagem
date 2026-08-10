

using AppDeMensagem.Feature.User.Model.Chat;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace AppDeMensagem.Feature.User.Services.Chat;

public class ChatHubService
{
    private readonly HubConnection _connection;

    public event Action<MessageChatModel>? OnMessageReceived;

    public bool IsConnected => _connection.State == HubConnectionState.Connected;

    public ChatHubService(IConfiguration configuration)
    {

        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7140/hubs/chat")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<MessageChatModel>(
            "ReceiveMessage",
            message =>
            {
                OnMessageReceived?.Invoke(message);
            });
    }


    public async Task StartAsync()
    {
        if(_connection.State == HubConnectionState.Disconnected)
        {
            await _connection.StartAsync();
        }
    }

    public async Task JoinChatAsync(Guid chatId)
    {
        if (!IsConnected)
            await StartAsync();

        await _connection.InvokeAsync(
            "JoinChatGroup",
            chatId
            );
    }

    public async Task LeaveChatAsync(Guid chatId)
    {
        if (!IsConnected)
            return;

        await _connection.InvokeAsync(
            "LeaveChatGroup",
            chatId);
    }


    public async Task StopAsync()
    {
        if(_connection.State != HubConnectionState.Disconnected)
        {
            await _connection.StopAsync();
        }
    }
}
