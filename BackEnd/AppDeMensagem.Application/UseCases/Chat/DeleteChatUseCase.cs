

using AppDeMensagem.Application.DTOs.UserChat.Response;
using AppDeMensagem.Application.Interfaces.Repositorys;
using AppDeMensagem.Domain.Entity;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Application.UseCases.Chat;

public class DeleteChatUseCase(
    IUserRepository userRepository,
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<ResponseUserChat> ExecuteAsync(Guid userId, Guid chatId)
    {
        var userChat = await chatRepository.FindByIdWithUserId(chatId, userId)
            ?? throw new NotFoundException("Chat not found");


        return new ResponseUserChat
        {
            UserChat_ID = userChat.UserChat_ID,
            IsAdmin = userChat.IsAdmin,
            Name = userChat.NameChat,
            Status = userChat.Status.ToString(),
            User_ID = userChat.User_ID,
            Chat = new ResponseChatInUserChat
            {
                Ativo = userChat.Chat.Ativo,
                Name = userChat.Chat.Name,
                Chat_ID = userChat.Chat.Chat_ID,
                Created = userChat.Chat.Created,
                UsersChat = userChat.Chat.UsersChat
                .Where(uc => uc.User_ID != userChat.User_ID)
                .Select(uc => new ResponseUserChat
                {
                    UserChat_ID = uc.UserChat_ID,
                    IsAdmin = uc.IsAdmin,
                    Name = uc.NameChat,
                    Status = uc.Status.ToString(),
                    User_ID = uc.User_ID,
                }).ToList()
            }
        };
    }
}
