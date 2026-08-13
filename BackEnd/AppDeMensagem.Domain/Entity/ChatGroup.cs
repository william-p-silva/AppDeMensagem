

namespace AppDeMensagem.Domain.Entity;

public class ChatGroup : Chat
{
    protected ChatGroup() { }
    public ChatGroup(Usuario user, string name) : base(ativo: true)
    {
        UserChat userChat = new UserChat(user, this, isAdmin: true, nameChat: name);
        AddParticipant(userChat);
        this.Name = name ?? "Sem nome";
    }

    public void AddPeopleInChat(Usuario user)
    {
        if (UsersChat.Any(uc => uc.User_ID == user.User_ID))
            throw new InvalidOperationException("The user is already in the chat. ");

        UserChat userChat = new UserChat(user, this, isAdmin: false, nameChat: user.UserName.TextName);
        AddParticipant(userChat);
    }
}
