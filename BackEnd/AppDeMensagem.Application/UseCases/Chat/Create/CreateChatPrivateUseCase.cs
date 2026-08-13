

using AppDeMensagem.Application.DTOs.Chat.Request;
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

    public async Task<string> ExecuteAsync(Guid userPrimary_Id, RequestNewChat request)
    {
        var userPrimary = await userRepository.FindById(userPrimary_Id);
        if (userPrimary is null)
            throw new ArgumentException("The user primary cannot be null. ");
        if (userPrimary.UserProfile == PerfilUser.Deleted)
            throw new ArgumentException("The user deleted. ");

        var userSecond = await userRepository.FindByEmail(request.Email);
        if (userSecond is null)
            throw new ArgumentException("The user second cannot be null. ");
        if (userSecond.UserProfile == PerfilUser.Deleted)
            throw new ArgumentException("The user deleted. ");

        var secondUserChatIDs = userSecond.UsersChat.Select(x => x.Chat_ID);

        var chatExist = userPrimary.UsersChat.Any(c => secondUserChatIDs.Contains(c.Chat_ID) && c.Chat is ChatPrivate);

        if (chatExist)
            throw new InvalidOperationException("A private chat already exists between these users.");

        ChatPrivate chatPrivate = new ChatPrivate(userPrimary, userSecond);

        await chatRepository.AddAsync(chatPrivate);

        await unitOfWork.CommitAsync();

        return "Chat criado";
    }
}
