

using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AppDeMensagem.Application.UseCases.Chat.Create;

public class CreateChatPrivateUseCase(
    IChatRepository chatRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    )
{

    public async Task<string> ExecuteAsync(Guid userPrimary_Id, RequestNewChat request)
    {
        var userPrimary = await userRepository.FindById(userPrimary_Id) 
            ?? throw new ArgumentException("The user primary cannot be null.");
        if (userPrimary.UserProfile == PerfilUser.Deleted)
            throw new ArgumentException("The user deleted. ");

        var userSecond = await userRepository.FindByEmail(request.Email) 
            ?? throw new ArgumentException("The user second cannot be null.");
        if (userSecond.UserProfile == PerfilUser.Deleted)
            throw new ArgumentException("The user deleted. ");

        var chatExist = await chatRepository.FindPrivateChatBetweenUsersAsync(
            userPrimary.User_ID, userSecond.User_ID);

        if (chatExist is not null)
        {
            if (chatExist.Status == StatusUserChat.Ativo)
                return "Chat já existe";

            chatExist.Ativar();
            await unitOfWork.CommitAsync();
            return "Chat reativado";
        }

        ChatPrivate chatPrivate = new ChatPrivate(userPrimary, userSecond);

        await chatRepository.AddAsync(chatPrivate);

        await unitOfWork.CommitAsync();

        return "Chat criado";
    }
}
