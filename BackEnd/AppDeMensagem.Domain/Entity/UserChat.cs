
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Domain.Entity;

public class UserChat 
{
    public Guid UserChat_ID { get; private set; }
    public Guid User_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public bool IsAdmin { get; private set; }
    public string NameChat { get; private set; }
    public StatusUserChat Status { get; private set; }

    private readonly List<Message> _messages = new List<Message>();

    public Chat Chat { get; private set; }
    public Usuario Usuario { get; private set; }
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();


    protected UserChat() { }

    public UserChat(Usuario user, Chat chat, bool isAdmin, string nameChat) 
    {
        if(user is null) 
            throw new ArgumentNullException("The user cannot be null. ");
        if (chat is null)
            throw new ArgumentNullException("The chat cannot be null. ");

        UserChat_ID = Guid.NewGuid();
        User_ID = user.User_ID;
        Chat_ID = chat.Chat_ID;
        IsAdmin = isAdmin;
        NameChat = nameChat;
        Chat = chat;
        Usuario = user;
        Status = StatusUserChat.Ativo;
    }

    public void PromoverAdmin()
    {
        if (Status != StatusUserChat.Ativo)
            throw new BusinessRuleException("A deleted user cannot be an admin.");
        if (IsAdmin)
            throw new ValidationException("The user is already an admin.");

        IsAdmin = true;
    }

    public void Desativar()
    {
        if (Status == StatusUserChat.Excluido)
            throw new BusinessRuleException("The user already deleted.");

        IsAdmin = false;
        Status = StatusUserChat.Excluido;
    }

    public void Ativar()
    {
        if (Status == StatusUserChat.Ativo)
            throw new BusinessRuleException($"The chat already active. {NameChat}");
        if (!Chat.Ativo && Chat is ChatPrivate)
            Chat.Ativar();

        Status = StatusUserChat.Ativo;
    }
}
