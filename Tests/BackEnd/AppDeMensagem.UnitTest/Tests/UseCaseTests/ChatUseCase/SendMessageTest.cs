

using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Services;
using AppDeMensagem.Application.UseCases.Chat;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.UnitTest.Fixtures;
using Moq;

namespace AppDeMensagem.UnitTest.Tests.UseCaseTests.ChatUseCase;

public class SendMessageTest
{
    private readonly ChatFixture _chatFixture = new();
    private readonly UserFixture _userFixture = new();
    private readonly Mock<IChatRepository> _chatMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IChatNotificationService> _chatNotificationServiceMock = new();

    [Fact]
    public async Task Sucesso_DadosValidos()
    {
        //Arrange
        Usuario userPrimary = _userFixture.CreateUserFake(
            email: "teste@gmail.com",
            userName: "Teste da Siva",
            password: "123456",
            profile: PerfilUser.User
            );

        Usuario userSecond = _userFixture.CreateUserFake(
            email: "testejr@gmail.com",
            userName: "Teste da Siva Junior",
            password: "123456",
            profile: PerfilUser.User
            );

        ChatPrivate chatPrivate = _chatFixture.CreateChatPrivateFake(userPrimary, userSecond);

        string messageText = "Olá teste da silva, como vc está?";

        RequestSendMessage request = _chatFixture.CreateRequestSendMessage(chatPrivate.Chat_ID, messageText);

        //Act
        _chatMock.Setup(x => x.GetByIdWithParticipantsAsync(chatPrivate.Chat_ID)).ReturnsAsync(chatPrivate);

        SendMessageUseCase sendMessageUseCase = new SendMessageUseCase(
            chatRepository: _chatMock.Object,
            unitOfWork: _unitOfWorkMock.Object,
            chatNotificationService: _chatNotificationServiceMock.Object
            );

        var result = await sendMessageUseCase.ExecuteAsync(request, userPrimary.User_ID);

        //Assert
        Assert.Equal(result.TextMessage, messageText);
        Assert.Equal(result.User_Id, userPrimary.User_ID);
        _chatMock.Verify(x => x.GetByIdWithParticipantsAsync(It.IsAny<Guid>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        _chatNotificationServiceMock.Verify(x => x.NotifyMessageSentAsync(
            It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
    }
}
