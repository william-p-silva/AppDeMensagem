
using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.UnitTest.Fixtures;

public class ChatFixture
{
    //criar um user valido para enviar a msg

    public ChatPrivate CreateChatPrivateFake(Usuario userPrimary, Usuario userSecond)
    {
        ChatPrivate chatPrivate = new ChatPrivate(userPrimary, userSecond);

        return chatPrivate;
    }

    public ChatGroup CreateChatGroup(Usuario user, string name = "teste")
    {
        ChatGroup chatGroup = new ChatGroup(user, name);

        return chatGroup;
    }

    public RequestSendMessage CreateRequestSendMessage(Guid chatId, string text)
    {
        return new RequestSendMessage
        {
            Chat_ID = chatId,
            TextMessage = text,
        };
    }
}
