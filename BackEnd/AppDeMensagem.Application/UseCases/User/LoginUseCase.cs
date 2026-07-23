

using AppDeMensagem.Application.DTOs.User.Request;
using AppDeMensagem.Application.DTOs.User.Response;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Security;

namespace AppDeMensagem.Application.UseCases.User;

public class LoginUseCase(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService
    )
{
    public async Task<ResponseLogin> ExecuteAsync(RequestLogin request)
    {
        var user = await userRepository.FindByEmail(request.Email);
        if (user is null || user.UserProfile == Domain.Enum.PerfilUser.Deleted)
            throw new ArgumentException("The user not exist or deleted. ", nameof(request.Email));
        if (!passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            throw new ArgumentException("The password not is match. ");

        string token = tokenService.GenereteToken(user);

        return new ResponseLogin
        {
            Email = user.EmailAddress.Endereco.ToString(),
            Name = user.UserName.TextName.ToString(),
            Profile = user.UserProfile.ToString(),
            Token = token
        };
    }
}
