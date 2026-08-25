
using AppDeMensagem.Application.DTOs.Message.Request;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Application.UseCases.Message;

public class MarkMessageAsReadUseCase(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMessageRepository messageRepository)
{

    public async Task ExecuteAsync(RequestMarkMessageRead request)
    {

        var messages = await messageRepository.GetByIDsToListAsync(request.Message_IDs);
        if (messages is null)
            throw new NotFoundException("Message doesn't. ");

        var reader = await userRepository.FindById(request.Reader_ID);
        if (reader is null)
            throw new NotFoundException("User not found. ");

        foreach (var message in messages)
        {
            message.MaskAsRead();
        }

        await unitOfWork.CommitAsync();

    }
}
