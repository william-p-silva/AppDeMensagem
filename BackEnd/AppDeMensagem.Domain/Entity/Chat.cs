

namespace AppDeMensagem.Domain.Entity;

public abstract class Chat
{
    public Guid Chat_ID { get; private set; }
    public DateTime Created { get; private set; }
    public bool Ativo { get; private set; }

    private readonly List<Message> _messages = new List<Message>();
    private readonly List<UserChat> _usersChat = new List<UserChat>();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
    public IReadOnlyCollection<UserChat> UsersChat => _usersChat.AsReadOnly();

    protected Chat () { }
    public Chat(bool ativo = true)
    {
        Chat_ID = Guid.NewGuid();
        Created = DateTime.UtcNow;
        Ativo = ativo;
    }

    protected void AddParticipant(UserChat userChat)
    {
        if(userChat.Chat_ID != Chat_ID)
            throw new InvalidOperationException("The chat does not match");

        _usersChat.Add(userChat);
    }

    public void SendMessage(UserChat sender, string text)
    {
        if(!UsersChat.Any(uc => uc.User_ID == sender.User_ID))
            throw new InvalidOperationException("The user or the chat does not match");
        if (!UsersChat.Any(uc => uc.Chat_ID == sender.Chat_ID))
            throw new InvalidOperationException("The user or the chat does not match");

        Message msg = new Message(this, sender, text);
        _messages.Add(msg);
    }
}
