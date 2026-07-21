

using AppDeMensagem.Domain.Enum;

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

        UserChat userChat1 = new UserChat(userPrimary, this, isAdmin: false);
        UserChat userChat2 = new UserChat(userSecond, this, isAdmin: false);

        AddParticipant(userChat1);
        AddParticipant(userChat2);
    }

}
