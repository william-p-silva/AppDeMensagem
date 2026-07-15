

using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Domain.Entity;

public class ChatPrivate : Chat
{
    public ChatPrivate(Guid user1_id, Guid user2_id)
    {

        UserChat userChat1 = new UserChat(user1_id, Chat_ID, isAdmin: false);
        UserChat userChat2 = new UserChat(user2_id, Chat_ID, isAdmin: false);

        UsersChat.Add(userChat1);
        UsersChat.Add(userChat2);
    }

}
