

using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.UnitTest.Tests.DomainTests.Chat;

public class ChatTest
{
    [Fact]
    public async Task SendMessage_QuandoSenderNaoParticipaDoChat_DeveLancarExcecao()
    {
        // Arrange
        var chat = new ChatGroup(Guid.NewGuid());
        var senderDeOutroChat = new UserChat(Guid.NewGuid(), Guid.NewGuid(), false);

        // Act
        Action act = () => chat.SendMessage(senderDeOutroChat, "oi");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CriarChatGroup_DeveGerarChatIdValido()
    {
        var chat = new ChatGroup(Guid.NewGuid());

        Assert.NotEqual(Guid.Empty, chat.Chat_ID);
        Assert.NotEqual(default, chat.Created);
    }
}
