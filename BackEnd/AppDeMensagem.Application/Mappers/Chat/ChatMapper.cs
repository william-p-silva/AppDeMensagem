
using AppDeMensagem.Application.DTOs.UserChat.Response;

namespace AppDeMensagem.Application.Mappers.Chat;

public static class ChatMapper
{
    public static ResponseUserChat ToResponse(
        this Domain.Entity.UserChat userChat)
    {
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
               .Select(uc => new ResponseUserChatSimplificado
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
