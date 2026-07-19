
using AppDeMensagem.Application.DTOs.User;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.UnitTest.Fixtures;

internal class UserFixture
{
    public Usuario CreateUserFake(
        string email,
        string userName,
        string password,
        PerfilUser profile
        )
    {
        Usuario user = new Usuario(
            email: email,
            userName: userName,
            passwordHash: password,
            userProfile: profile
            );

        return user;
    }

    public RequestRegister CreateRequestRegisterUser(
        string email,
        string userName,
        string password,
        PerfilUser profile
        )
    {
        return new RequestRegister
        {
            Email = email,
            Name = userName,
            PassoWord = password,
            Profile = profile
        };
    }
}
