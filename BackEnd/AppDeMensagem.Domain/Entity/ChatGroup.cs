

namespace AppDeMensagem.Domain.Entity;

public class ChatGroup : Chat
{
    protected ChatGroup() { }
    public ChatGroup(Usuario userPrimary, List<Usuario> users, string name) : base(ativo: true)
    {
        if(userPrimary is null)
            throw new InvalidOperationException("At least one user is required. ");

        if (users is null || users.Count <= 0)
            throw new InvalidOperationException("At least one user is required.");

        if (users.Any(u => u.User_ID == userPrimary.User_ID))
            throw new ArgumentException(
                "The primary user cannot be included in the users list.",
                nameof(users)
            );

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The name chat group cannot be null. ");

        foreach (Usuario user in users)
        {
            UserChat userChatSecond = new UserChat(user, this, isAdmin: false, nameChat: name);
            AddParticipant(userChatSecond);
        }

        UserChat userChatPrimary = new UserChat(userPrimary, this, isAdmin: true, nameChat: name);
        AddParticipant(userChatPrimary);

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
