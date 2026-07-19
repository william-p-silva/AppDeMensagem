
namespace AppDeMensagem.Domain.Entity;

public class UserChat 
{
    public Guid UserChat_ID { get; private set; }
    public Guid User_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public bool IsAdmin { get; private set; }

    private readonly List<Message> _messages = new List<Message>();

    public Chat Chat { get; private set; }
    public Usuario Usuario { get; private set; }
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();


    protected UserChat() { }

    public UserChat(Guid user_id, Guid chat_id, bool isAdmin) 
    {
        if(user_id == Guid.Empty) 
            throw new ArgumentNullException(nameof(user_id), "The id user cannot be null. ");
        if (chat_id == Guid.Empty)
            throw new ArgumentNullException(nameof(user_id), "The id chat cannot be null. ");

        UserChat_ID = Guid.NewGuid();
        User_ID = user_id;
        Chat_ID = chat_id;
        IsAdmin = isAdmin;
    }
}
