

using AppDeMensagem.Domain.Enum;
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

    public override void Exit(UserChat userChat)
    {
        if (userChat is null)
            throw new ValidationException("The user cannot be null.");
        if (!UsersChat.Any(uc =>
            uc.UserChat_ID == userChat.UserChat_ID &&
            uc.Status == StatusUserChat.Ativo))        
            throw new BusinessRuleException("The user is not an active participant.");
        

        if (!Ativo)
            throw new BusinessRuleException("The chat is not active.");

        if (
            userChat.IsAdmin
            && UsersChat.Count(uc =>
                uc.IsAdmin &&
                uc.Status == StatusUserChat.Ativo) <= 1
            && ActiveParticipantsCount > 1
        )
        {
            var newAdmin = UsersChat.FirstOrDefault(
                uc => uc.User_ID != userChat.User_ID
                    && uc.Status == StatusUserChat.Ativo
            );

            if (newAdmin is null)
                throw new NotFoundException(
                    "There is no active user to replace the admin."
                );

            newAdmin.PromoverAdmin();
        }

        RemoveParticipant(userChat);

        if (ActiveParticipantsCount <= 0)
            Ativo = false;
    }
}
