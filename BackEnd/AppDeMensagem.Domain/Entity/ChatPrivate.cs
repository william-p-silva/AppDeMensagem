

using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Domain.Entity;

public class ChatPrivate : Chat
{
    public ChatPrivate(Guid user1_id, Guid user2_id)
    {
        if (user1_id == user2_id)
            throw new InvalidOperationException("The user cannot create chat with himself. ");

        UserChat userChat1 = new UserChat(user1_id, Chat_ID, isAdmin: false);
        UserChat userChat2 = new UserChat(user2_id, Chat_ID, isAdmin: false);

        AddParticipant(userChat1);
        AddParticipant(userChat2);
    }

}
