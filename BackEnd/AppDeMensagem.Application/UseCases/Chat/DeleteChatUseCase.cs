

using AppDeMensagem.Application.DTOs.UserChat.Response;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Application.Mappers.Chat;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Application.UseCases.Chat;

public class DeleteChatUseCase(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<ResponseUserChat> ExecuteAsync(Guid userId, Guid chatId)
    {
        var userChat = await chatRepository.FindByIdWithUserId(chatId, userId)
            ?? throw new NotFoundException("Chat not found");

        var chat = userChat.Chat;

        chat.Exit(userChat);

        await unitOfWork.CommitAsync();

        return userChat.ToResponse();
    }
}
