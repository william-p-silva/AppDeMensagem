

using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Application.UseCases.Chat.Create;

public class CreateChatPrivateUseCase(
    IChatRepository chatRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    )
{

    public async Task<string> ExecuteAsync(Guid userPrimary_Id, Guid userSecond_Id)
    {
        var userPrimary = await userRepository.FindById(userPrimary_Id);
        if (userPrimary is null)
            throw new ArgumentException("The user primary cannot be null. ");
        if (userPrimary.UserProfile == PerfilUser.Deleted)
            throw new ArgumentException("The user deleted. ");

        var userSecond = await userRepository.FindById(userSecond_Id);
        if (userSecond is null)
            throw new ArgumentException("The user second cannot be null. ");
        if (userSecond.UserProfile == PerfilUser.Deleted)
            throw new ArgumentException("The user deleted. ");

        ChatPrivate chatPrivate = new ChatPrivate(userPrimary, userSecond);

        await chatRepository.AddAsync(chatPrivate);

        await unitOfWork.CommitAsync();

        return "Chat criado";
    }
}
