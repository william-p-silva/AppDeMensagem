
using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Domain.Entity;

public abstract class Chat
{
    public Guid Chat_ID { get; private set; }
    public DateTime Created { get; private set; }

    public ICollection<Message> Messages { get; private set; } = new List<Message>();
    public ICollection<UserChat> UsersChat { get; private set; } = new List<UserChat>();


    public Chat()
    {
        Chat_ID = Guid.NewGuid();
        Created = DateTime.UtcNow;
    }

    public void SendMessage(string text)
    {
        Message msg = new Message(Chat_ID, text);
        Messages.Add(msg);
    }
}
