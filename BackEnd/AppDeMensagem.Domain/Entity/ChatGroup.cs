

namespace AppDeMensagem.Domain.Entity;

public class ChatGroup : Chat
{
    protected ChatGroup() { }
    public ChatGroup(Usuario user) : base(ativo: true)
    {
        UserChat userChat = new UserChat(user, this, isAdmin: true);
        AddParticipant(userChat);
    }

    public void AddPeopleInChat(Usuario user)
    {
        if (UsersChat.Any(uc => uc.User_ID == user.User_ID))
            throw new InvalidOperationException("The user is already in the chat. ");

        UserChat userChat = new UserChat(user, this, isAdmin: false);
        AddParticipant(userChat);
    }
}
