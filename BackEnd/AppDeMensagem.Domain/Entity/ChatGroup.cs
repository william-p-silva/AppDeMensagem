

namespace AppDeMensagem.Domain.Entity;

public class ChatGroup : Chat
{
    protected ChatGroup() { }
    public ChatGroup(Guid user_id)
    {
        UserChat userChat = new UserChat(user_id, Chat_ID, isAdmin: true);
        AddParticipant(userChat);
    }

    public void AddPeopleInChat(Guid user_id)
    {
        if (UsersChat.Any(uc => uc.User_ID == user_id))
            throw new InvalidOperationException("The user is already in the chat. ");

        UserChat userChat = new UserChat(user_id, Chat_ID, isAdmin: false);
        AddParticipant(userChat);
    }
}
