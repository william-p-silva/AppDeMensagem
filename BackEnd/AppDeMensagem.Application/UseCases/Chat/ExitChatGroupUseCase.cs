

using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Application.UseCases.Chat;

public class ExitChatGroupUseCase(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<string> ExecuteAsync(RequestExiteChatGroup request, Guid user_id)
    {
        var chatGroup = await chatRepository.FindChatGroupByIdAsync(
            chat_id: request.Chat_ID, user_id: user_id, ativo: true);

        if (chatGroup is null)
            throw new NotFoundException("Chat not found or the user is not a member.");

        var userChat = chatGroup.UsersChat.FirstOrDefault(uc => uc.User_ID == user_id);

        if (userChat is null)
            throw new NotFoundException("The user not member in chat. ");

        chatGroup.Exit(userChat);

        await unitOfWork.CommitAsync();

        return "Removido com sucesso.";
    }
}
