
using AppDeMensagem.Application.DTOs.User.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Security;
using AppDeMensagem.Application.UseCases.User;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.UnitTest.Fixtures;
using Moq;

namespace AppDeMensagem.UnitTest.Tests.UseCaseTests.UserTests;

public class LoginUserTest
{
    private readonly UserFixture userFixture = new UserFixture();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();

    [Fact]
    public async Task RetornarSucesso_DadosValidos()
    {
        //Arrange
        string emailFake = "testvalid@gmail.com";
        string userNameFake = "Name Valid of Silva";
        string passwordFake = "123";
        PerfilUser profile = PerfilUser.User;

        Usuario user = userFixture.CreateUserFake(
            email: emailFake,
            userName: userNameFake,
            password: passwordFake,
            profile: profile
            );

        RequestLogin requestLogin = userFixture.CreateRequestLogin(emailFake, passwordFake);

        //Act
        _userRepositoryMock.Setup(x => x.FindByEmail(requestLogin.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(x => x.VerifyPassword(requestLogin.Password, user.PasswordHash)).Returns(true);
        _tokenServiceMock.Setup(x => x.GenereteToken(user)).Returns("Token");

        LoginUseCase loginUseCase = new LoginUseCase(
            userRepository: _userRepositoryMock.Object,
            passwordHasher: _passwordHasherMock.Object,
            tokenService: _tokenServiceMock.Object
            );

        var result = await loginUseCase.ExecuteAsync(requestLogin);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.Email, emailFake);
        Assert.Equal(result.Name, userNameFake);
        Assert.Equal(result.Profile, profile.ToString());
        Assert.Equal(result.Token, "Token");
        _passwordHasherMock.Verify(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _userRepositoryMock.Verify(x => x.FindByEmail(It.IsAny<string>()), Times.Once());
        _tokenServiceMock.Verify(x => x.GenereteToken(It.IsAny<Usuario>()), Times.Once());
    }
}
