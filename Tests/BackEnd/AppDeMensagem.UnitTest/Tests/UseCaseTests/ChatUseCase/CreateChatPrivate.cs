
using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.UseCases.Chat.Create;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.UnitTest.Fixtures;
using Moq;

namespace AppDeMensagem.UnitTest.Tests.UseCaseTests.ChatUseCase;

public class CreateChatPrivate
{
    private readonly UserFixture _userFixture = new UserFixture();
    private readonly Mock<IChatRepository> _chatRepository = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task DeveRetornarSucesso_DadosValidos()
    {
        // Arrange
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        var user1 = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        var user2 = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        //Act
        _userRepository.Setup(x => x.FindById(user1.User_ID)).ReturnsAsync(user1);
        _userRepository.Setup(x => x.FindByEmail(user2.EmailAddress.Endereco.ToString())).ReturnsAsync(user2);

        CreateChatPrivateUseCase createChatPrivate = new CreateChatPrivateUseCase(
            _chatRepository.Object,
            _userRepository.Object,
            _unitOfWork.Object
            );

        string result = await createChatPrivate.ExecuteAsync(user1.User_ID, new RequestNewChat { Email = user2.EmailAddress.Endereco.ToString() });

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result, "Chat criado");
        _userRepository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
        _userRepository.Verify(x => x.FindByEmail(It.IsAny<string>()), Times.Once);
        _chatRepository.Verify(x => x.AddAsync(It.IsAny<ChatPrivate>() ), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task DeveGerarExcecao_UserIguais()
    {
        // Arrange
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        var user1 = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        //Act
        _userRepository.Setup(x => x.FindById(user1.User_ID)).ReturnsAsync(user1);
        _userRepository.Setup(x => x.FindByEmail(user1.EmailAddress.Endereco.ToString())).ReturnsAsync(user1);

        CreateChatPrivateUseCase createChatPrivate = new CreateChatPrivateUseCase(
            _chatRepository.Object,
            _userRepository.Object,
            _unitOfWork.Object
            );

        var result = await Assert.ThrowsAsync<InvalidOperationException>(() => createChatPrivate.ExecuteAsync(user1.User_ID, new RequestNewChat { Email = user1.EmailAddress.Endereco.ToString() }));

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Message, "The user cannot create chat with himself. ");
        _userRepository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
        _userRepository.Verify(x => x.FindByEmail(It.IsAny<string>()), Times.Once);
        _chatRepository.Verify(x => x.AddAsync(It.IsAny<ChatPrivate>()), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task DeveGerarExcecao_UserPrimaryNull()
    {
        // Arrange
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        var user1 = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        Guid userPrimaryFake = Guid.NewGuid();

        //Act
        _userRepository.Setup(x => x.FindById(userPrimaryFake)).ReturnsAsync((Usuario?) null);

        CreateChatPrivateUseCase createChatPrivate = new CreateChatPrivateUseCase(
            _chatRepository.Object,
            _userRepository.Object,
            _unitOfWork.Object
            );

        var result = await Assert.ThrowsAsync<ArgumentException>(() => createChatPrivate.ExecuteAsync(user1.User_ID, new RequestNewChat { Email = userPrimaryFake.ToString() }));

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Message, "The user primary cannot be null. ");
        _userRepository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
        _chatRepository.Verify(x => x.AddAsync(It.IsAny<ChatPrivate>()), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task DeveGerarExcecao_UserSecondNull()
    {
        // Arrange
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        var user1 = _userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        string userSecondFake = "testinvalid@gmail.com";

        //Act
        _userRepository.Setup(x => x.FindById(user1.User_ID)).ReturnsAsync(user1);

        _userRepository.Setup(x => x.FindByEmail(userSecondFake.ToString())).ReturnsAsync((Usuario?)null);

        CreateChatPrivateUseCase createChatPrivate = new CreateChatPrivateUseCase(
            _chatRepository.Object,
            _userRepository.Object,
            _unitOfWork.Object
            );

        var result = await Assert.ThrowsAsync<ArgumentException>(() => createChatPrivate.ExecuteAsync(user1.User_ID, new RequestNewChat { Email = userSecondFake.ToString() }));

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Message, "The user second cannot be null. ");
        _userRepository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
        _userRepository.Verify(x => x.FindByEmail(It.IsAny<string>()), Times.Once);
        _chatRepository.Verify(x => x.AddAsync(It.IsAny<ChatPrivate>()), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }
}
