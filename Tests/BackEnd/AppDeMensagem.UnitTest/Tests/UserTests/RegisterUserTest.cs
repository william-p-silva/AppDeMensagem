

using AppDeMensagem.Application.DTOs.User;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Security;
using AppDeMensagem.Application.UseCases.User;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.UnitTest.Fixtures;
using Moq;

namespace AppDeMensagem.UnitTest.Tests.UserTests;

public class RegisterUserTest
{
    private readonly UserFixture userFixture = new UserFixture();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [Fact]
    public async Task ReturnSuccess_OnValidDados()
    {
        //Arrange
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        RequestRegister requestRegisterFake = userFixture.CreateRequestRegisterUser(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        //Act
        _passwordHasherMock.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(passwordFake);
        _userRepositoryMock.Setup(x => x.FindByEmail(requestRegisterFake.Email)).ReturnsAsync((Usuario?) null);

        var registerUseCase = new RegisterUseCase(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _unitOfWorkMock.Object
            );

        var user = await registerUseCase.Execute(requestRegisterFake);

        //Assert
        Assert.NotNull(user);
        Assert.Equal(user, "Registration successfuly completed. ");
        _passwordHasherMock.Verify(x => x.HashPassword(It.IsAny<string>()), Times.Once());
        _userRepositoryMock.Verify(x => x.FindByEmail(requestRegisterFake.Email), Times.Once());
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once());
    }
}
