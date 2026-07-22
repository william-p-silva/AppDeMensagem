using AppDeMensagem.Application.DTOs.User.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Interfaces.Security;
using AppDeMensagem.Domain.Entity;

namespace AppDeMensagem.Application.UseCases.User;

public class RegisterUseCase(
    IUserRepository userRepository, 
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
{
    public async Task<string> Execute(RequestRegister request)
    {
        var userExist = await userRepository.FindByEmail(request.Email);
        if (userExist is not null)
            throw new ArgumentException("User already registed. ", nameof(request.Email));

        string hash = passwordHasher.HashPassword(request.PassWord);

        Usuario user = new Usuario(
            email: request.Email,
            userName: request.Name,
            passwordHash: hash,
            userProfile: request.Profile
            );

        await userRepository.AddAsync(user);
        await unitOfWork.CommitAsync();


        return "Registration successfuly completed. ";
    }
}
