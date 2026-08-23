

using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Domain.Entity;

public class ChatGroup : Chat
{
    protected ChatGroup() { }
    public ChatGroup(Usuario userPrimary, List<Usuario> users, string name) : base(ativo: true)
    {
        if (userPrimary is null)
            throw new ValidationException("At least one user is required.");

        if (users is null || users.Count <= 0)
            throw new ValidationException("At least one user is required.");

        if (users.Any(u => u.User_ID == userPrimary.User_ID))
            throw new BusinessRuleException("The primary user cannot be included in the users list.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("The name chat group cannot be null. ");

        foreach (Usuario user in users)
        {
            UserChat userChatSecond = new UserChat(user, this, isAdmin: false, nameChat: name);
            AddParticipant(userChatSecond);
        }

        UserChat userChatPrimary = new UserChat(userPrimary, this, isAdmin: true, nameChat: name);
        AddParticipant(userChatPrimary);

        Name = name;
    }

    public void AddPeopleInChat(Usuario user)
    {
        if (UsersChat.Any(uc => uc.User_ID == user.User_ID))
            throw new BusinessRuleException("The user is already in the chat. ");

        UserChat userChat = new UserChat(user, this, isAdmin: false, nameChat: user.UserName.TextName);
        AddParticipant(userChat);
    }

    public void Exit(UserChat userChat)
    {
        if (userChat is null || !UsersChat.Any(uc => uc.User_ID == userChat.User_ID && uc.Chat_ID == Chat_ID))
            throw new BusinessRuleException("The user is not in the chat. ");

        if (UsersChat.Count <= 1)
            throw new BusinessRuleException("The chat has only one user, and they cannot leave the chat.");

        if (userChat.IsAdmin)
        {
            if (UsersChat.Count(a => a.IsAdmin) <= 1)
            {
                var userChatNewAdmin = UsersChat.FirstOrDefault(u => u.User_ID != userChat.User_ID);
                if (userChatNewAdmin is null)
                    throw new NotFoundException("There is no user to replace the admin. ");

                userChatNewAdmin.PromoverAdmin();

            }
        }

        RemoveParticipant(userChat);
    }
}
