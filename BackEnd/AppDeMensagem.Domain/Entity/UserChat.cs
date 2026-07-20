
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

    public UserChat(Usuario user, Chat chat, bool isAdmin) 
    {
        if(user is null) 
            throw new ArgumentNullException("The user cannot be null. ");
        if (chat is null)
            throw new ArgumentNullException("The chat cannot be null. ");

        UserChat_ID = Guid.NewGuid();
        User_ID = user.User_ID;
        Chat_ID = chat.Chat_ID;
        IsAdmin = isAdmin;
        Chat = chat;
        Usuario = user;
    }
}
