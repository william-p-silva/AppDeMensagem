

using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.UnitTest.Fixtures;

namespace AppDeMensagem.UnitTest.Tests.DomainTests.Chat;

public class ChatTest()
{
    private readonly UserFixture _userFixture = new UserFixture();
    [Fact]
    public async Task SendMessage_QuandoSenderNaoParticipaDoChat_DeveLancarExcecao()
    {
        // Arrange
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        var user = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        var userFalse = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        ChatGroup chat = new ChatGroup(user);
        var senderDeOutroChat = new UserChat(userFalse, chat, false);

        // Act
        Action act = () => chat.SendMessage(senderDeOutroChat, "oi");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CriarChatGroup_DeveGerarChatIdValido()
    {
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        var user = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );
        ChatGroup chat = new ChatGroup(user);

        Assert.NotEqual(Guid.Empty, chat.Chat_ID);
        Assert.NotEqual(default, chat.Created);
    }
}
