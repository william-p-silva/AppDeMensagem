

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.UseCases.Chat.Create;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.UnitTest.Fixtures;
using Moq;

namespace AppDeMensagem.UnitTest.Tests.UseCaseTests.ChatUseCase;

public class CreateChatGroupTest
{
    private readonly UserFixture _userFixture = new();
    private readonly ChatFixture _chatFixture = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IChatRepository> _chatRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();


    [Fact]
    public async Task QuandoUserValidos_DeveRetornarIdDoChat()
    {
        //Arrange
        var userPimary = _userFixture.CreateUserFake(
                            email: "userprimary@domus.com",
                            userName: "User Primary",
                            password: "123123",
                            profile: PerfilUser.User
                            );

        var userSecond = _userFixture.CreateUserFake(
                            email: "usersecond@domus.com",
                            userName: "User Second",
                            password: "123123",
                            profile: PerfilUser.User
                            );

        var userThird = _userFixture.CreateUserFake(
                            email: "userthird@domus.com",
                            userName: "User Third",
                            password: "123123",
                            profile: PerfilUser.User
                            );

        var request = _chatFixture.CreateRequestChatGroup("Teste", new List<Guid> { userSecond.User_ID, userThird.User_ID });

        var listUsers = new List<Usuario> { userSecond, userThird };

        //Act
        _userRepositoryMock.Setup(x => x.FindById(userPimary.User_ID)).ReturnsAsync(userPimary);
        foreach (var user in listUsers)
        {
            _userRepositoryMock.Setup(x => x.FindById(user.User_ID)).ReturnsAsync(user);
        }

        var useCase = new CreateChatGroupUseCase(
                        _chatRepositoryMock.Object,
                        _userRepositoryMock.Object,
                        _unitOfWorkMock.Object
                        );

        var response = await useCase.ExecuteAsync(
            userPimary.User_ID,
            request
            );

        //Asserts
        int totalIds = request.Users_IDs.Count + 1;
        Assert.IsType<Guid>( response );
        _userRepositoryMock.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Exactly(totalIds));
        _chatRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Chat>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }
}
