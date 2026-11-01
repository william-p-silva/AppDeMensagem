
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.UseCases.Chat;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Domain.Exceptions;
using AppDeMensagem.UnitTest.Fixtures;
using Moq;

namespace AppDeMensagem.UnitTest.Tests.UseCaseTests.ChatUseCase;

public class DeleteChatUseCaseTests
{
    private readonly ChatFixture _chatFixture = new();
    private readonly UserFixture _userFixture = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IChatRepository> _chatRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task DeveRetornarSucesso_QuandoUsuarioParticipaDoChatPrivate()
    {
        //Arrange
        Usuario userPrimary = _userFixture.CreateUserFake();
        Usuario userSecond = _userFixture.CreateUserFake();

        ChatPrivate chatFake = _chatFixture.CreateChatPrivateFake(
            userPrimary, userSecond);

        _chatRepository
            .Setup(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID
                ))
            .ReturnsAsync(
                chatFake.UsersChat.Single(
                    uc => uc.User_ID == userPrimary.User_ID
                )
            );

        _unitOfWork
            .Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

        //Act
        var deleteChatUseCase = new DeleteChatUseCase(
            _chatRepository.Object,
            _unitOfWork.Object);

        var result = await deleteChatUseCase.ExecuteAsync(
            userId: userPrimary.User_ID, chatId: chatFake.Chat_ID);

        //Assert
        Assert.NotNull(result);

        var secondUser = result.Chat.UsersChat
            .SingleOrDefault(uc => uc.User_ID == userSecond.User_ID);

        Assert.Equal(StatusUserChat.Excluido.ToString(), result.Status);
        Assert.Equal(StatusUserChat.Ativo.ToString(), secondUser?.Status);
        //Apenas se o chat for privado, o chat será desativado
        Assert.False(result.Chat.Ativo);

        _chatRepository
            .Verify(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID),
                Times.Once);

        _unitOfWork
            .Verify(x => x.CommitAsync(),
                Times.Once);
    }

    [Fact]
    public async Task DeveRetornarSucesso_QuandoUsuarioParticipaDoChatGroupENaoForUltimo()
    {
        //Arrange
        Usuario userPrimary = _userFixture.CreateUserFake();

        List<Usuario> users = new List<Usuario>
        {
            _userFixture.CreateUserFake(),
            _userFixture.CreateUserFake()
        };

        ChatGroup chatFake = _chatFixture.CreateChatGroup(
            userPrimary, users);

        _chatRepository
            .Setup(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID
                ))
            .ReturnsAsync(
                chatFake.UsersChat.Single(
                    uc => uc.User_ID == userPrimary.User_ID
                )
            );

        _unitOfWork
            .Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

        //Act
        var deleteChatUseCase = new DeleteChatUseCase(
            _chatRepository.Object,
            _unitOfWork.Object);

        var result = await deleteChatUseCase.ExecuteAsync(
            userId: userPrimary.User_ID, chatId: chatFake.Chat_ID);

        //Assert
        Assert.NotNull(result);

        var otherUsers = result.Chat.UsersChat
            .Where(uc => uc.User_ID != userPrimary.User_ID)
            .ToList();

        Assert.Equal(StatusUserChat.Excluido.ToString(), result.Status);
        Assert.All(otherUsers, uc =>
            Assert.Equal(StatusUserChat.Ativo.ToString(), uc.Status));
        Assert.Equal(2, otherUsers.Count);

        //Se o chat for do tipo grupo, o chat não será desativado, apenas o usuário que solicitou a exclusão será removido do chat
        Assert.True(result.Chat.Ativo);

        _chatRepository
            .Verify(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID),
                Times.Once);

        _unitOfWork
            .Verify(x => x.CommitAsync(),
                Times.Once);
    }

    [Fact]
    public async Task DeveRetornarSucesso_QuandoUsuarioParticipaDoChatGroupEForUltimo()
    {
        //Arrange
        Usuario userPrimary = _userFixture.CreateUserFake();
        Usuario user1 = _userFixture.CreateUserFake();

        List<Usuario> users = new List<Usuario>
        {
            user1,
        };

        ChatGroup chatFake = _chatFixture.CreateChatGroup(
            userPrimary, users);

        chatFake.Exit(chatFake.UsersChat.Single(uc => uc.User_ID == user1.User_ID));

        _chatRepository
            .Setup(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID
                ))
            .ReturnsAsync(
                chatFake.UsersChat.Single(
                    uc => uc.User_ID == userPrimary.User_ID
                )
            );

        _unitOfWork
            .Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

        //Act
        var deleteChatUseCase = new DeleteChatUseCase(
            _chatRepository.Object,
            _unitOfWork.Object);

        var result = await deleteChatUseCase.ExecuteAsync(
            userId: userPrimary.User_ID,
            chatId: chatFake.Chat_ID);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(StatusUserChat.Excluido.ToString(), result.Status);
        Assert.DoesNotContain(
            result.Chat.UsersChat,
            uc => uc.Status == StatusUserChat.Ativo.ToString()
        );

        //Se o chat for do tipo grupo, o chat só será desativado, quando o usuário que solicitou a exclusão for o último participante do chat
        Assert.False(result.Chat.Ativo);

        _chatRepository
            .Verify(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID),
                Times.Once);

        _unitOfWork
            .Verify(x => x.CommitAsync(),
                Times.Once);
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoUsuarioNaoParticiparDoChat()
    {        //Arrange
        Usuario userPrimary = _userFixture.CreateUserFake();
        Usuario userParticipante = _userFixture.CreateUserFake();

        List<Usuario> users = new List<Usuario>
        {
            _userFixture.CreateUserFake()
        };

        ChatGroup chatFake = _chatFixture.CreateChatGroup(
            userParticipante,
            users
        );

        _chatRepository
            .Setup(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID
                ))
            .ReturnsAsync(
                (UserChat?)null
            );

        //Act
        var deleteChatUseCase = new DeleteChatUseCase(
            _chatRepository.Object,
            _unitOfWork.Object);

        var result = await Assert.ThrowsAsync<NotFoundException>(() =>  
            deleteChatUseCase.ExecuteAsync(
                userId: userPrimary.User_ID, chatId: chatFake.Chat_ID));

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Chat not found", result.Message);


        _chatRepository
            .Verify(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID),
                Times.Once);

        _unitOfWork
            .Verify(x => x.CommitAsync(),
                Times.Never);
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoVinculoDoUsuarioJaEstiverExcluido()
    {
        //Arrange
        Usuario userPrimary = _userFixture.CreateUserFake();
        Usuario userSecond = _userFixture.CreateUserFake();

        ChatPrivate chatFake = _chatFixture.CreateChatPrivateFake(
            userPrimary, userSecond);

        chatFake.UsersChat.Single(uc => uc.User_ID == userPrimary.User_ID).Desativar();

        _chatRepository
            .Setup(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID
                ))
            .ReturnsAsync(
                chatFake.UsersChat.Single(
                    uc => uc.User_ID == userPrimary.User_ID
                )
            );

        _unitOfWork
            .Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

        //Act
        var deleteChatUseCase = new DeleteChatUseCase(
            _chatRepository.Object,
            _unitOfWork.Object);

        var result = await Assert.ThrowsAsync<BusinessRuleException>(() =>
            deleteChatUseCase.ExecuteAsync(
                userId: userPrimary.User_ID, chatId: chatFake.Chat_ID));

        //Assert
        Assert.NotNull(result);
        Assert.Equal("The user already deleted.", result.Message);

        _chatRepository
            .Verify(x => x.FindByIdWithUserId(
                chatFake.Chat_ID,
                userPrimary.User_ID),
                Times.Once);

        _unitOfWork
            .Verify(x => x.CommitAsync(),
                Times.Never);
    }
}