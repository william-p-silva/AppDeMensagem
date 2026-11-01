

using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Domain.Entity;

public class ChatPrivate : Chat
{
    protected ChatPrivate() { }
    public ChatPrivate(Usuario userPrimary, Usuario userSecond) : base(true)
    {
        if (userPrimary.User_ID == userSecond.User_ID)
            throw new InvalidOperationException("The user cannot create chat with himself. ");
        if (userPrimary.UserProfile == PerfilUser.Deleted)
            throw new ArgumentException("The user primary deleted. ");
        if (userSecond.UserProfile == PerfilUser.Deleted)
        {
            throw new ArgumentException("The user second deleted. ");
        }

        UserChat userChat1 = new UserChat(userPrimary, this, isAdmin: false, nameChat: userSecond.UserName.TextName);
        UserChat userChat2 = new UserChat(userSecond, this, isAdmin: false, nameChat: userPrimary.UserName.TextName);

        AddParticipant(userChat1);
        AddParticipant(userChat2);

        Name = userSecond.UserName.TextName;
    }

    public override void Exit(UserChat userChat)
    {
        if (userChat is null)
            throw new ValidationException("The user cannot be null.");

        if (!UsersChat.Any(uc 
            => uc.User_ID == userChat.User_ID && uc.Chat_ID == Chat_ID))
            throw new ValidationException("The user is not in the chat.");

        if (!Ativo)
            throw new BusinessRuleException("The chat is not active.");

        RemoveParticipant(userChat);
        Ativo = false;
    }
}
