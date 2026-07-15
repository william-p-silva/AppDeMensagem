
using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Domain.Entity;

public abstract class Chat
{
    public Guid Chat_ID { get; private set; }
    public DateTime Created { get; private set; }

    private readonly List<Message> _messages = new List<Message>();
    private readonly List<UserChat> _usersChat = new List<UserChat>();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
    public IReadOnlyCollection<UserChat> UsersChat => _usersChat.AsReadOnly();


    public Chat()
    {
        Chat_ID = Guid.NewGuid();
        Created = DateTime.UtcNow;
    }

    protected void AddParticipant(UserChat userChat)
    {
        _usersChat.Add(userChat);
    }

    public void SendMessage(string text)
    {
        Message msg = new Message(Chat_ID, text);
        _messages.Add(msg);
    }
}
