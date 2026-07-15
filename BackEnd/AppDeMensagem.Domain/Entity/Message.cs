
using AppDeMensagem.Domain.Enum;

namespace AppDeMensagem.Domain.Entity;

public class Message
{
    public Guid Message_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public string Text { get; private set; }
    public DateTime SendTime { get; private set; }
    public StatusMessage Status { get; private set; }

    public Chat Chat { get; private set; }

    protected Message() { }

    public Message(
        Guid chat_id, string text)
    {
        if (string.IsNullOrWhiteSpace(text)) 
            throw new ArgumentNullException("The text cannot be null. ", nameof(text));
        if(chat_id == Guid.Empty) 
            throw new ArgumentNullException("The id chat cannot be null. ", nameof(chat_id));

        Text = text;
        SendTime = DateTime.UtcNow; 
        Status = StatusMessage.Sent;
        Chat_ID = chat_id;
    }
}
