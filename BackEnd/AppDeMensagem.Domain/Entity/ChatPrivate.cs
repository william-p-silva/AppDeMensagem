

using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Domain.Entity;

public class ChatPrivate : Chat
{
    protected ChatPrivate() { }
    public ChatPrivate(Usuario user1, Usuario user2) : base(true)
    {
        if (user1.User_ID == user2.User_ID)
            throw new InvalidOperationException("The user cannot create chat with himself. ");

        UserChat userChat1 = new UserChat(user1, this, isAdmin: false);
        UserChat userChat2 = new UserChat(user2, this, isAdmin: false);

        AddParticipant(userChat1);
        AddParticipant(userChat2);
    }

}
