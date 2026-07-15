
namespace AppDeMensagem.Domain.Entity;

public class UserChat
{
    public Guid UserChat_ID { get; private set; }
    public Guid User_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public bool IsAdmin { get; private set; }

    public Chat Chat { get; private set; }
    public Usuario Usuario { get; private set; }

    protected UserChat() { }

    public UserChat(Guid user_id, Guid chat_id, bool isAdmin)
    {
        if(user_id == Guid.Empty) 
            throw new ArgumentNullException("The id user cannot be null. ", nameof(user_id));
        if (chat_id == Guid.Empty)
            throw new ArgumentNullException("The id chat cannot be null. ", nameof(chat_id));

        UserChat_ID = Guid.NewGuid();
        User_ID = user_id;
        Chat_ID = chat_id;
        IsAdmin = isAdmin;
    }
}
