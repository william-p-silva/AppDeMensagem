

namespace AppDeMensagem.Domain.Entity;

public class ChatGroup : Chat
{
    protected ChatGroup() { }
    public ChatGroup(Guid user_id)
    {
        UserChat userChat = new UserChat(user_id, Chat_ID, isAdmin: true);
        UsersChat.Add(userChat);
    }

    public void AddPeopleInChat(Guid user_id)
    {
        UserChat userChat = new UserChat(user_id, Chat_ID, isAdmin: false);
        UsersChat.Add(userChat);
    }
}
