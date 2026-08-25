
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.Domain.Entity;

public class Message
{
    public Guid Message_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public Guid Sender_ID { get; private set; }
    public string Text { get; private set; }
    public DateTime SendTime { get; private set; }
    public StatusMessage Status { get; private set; }

    public UserChat Sender { get; private set; }
    public Chat Chat { get; private set; }

    protected Message() { }

    public Message(
        Chat chat, UserChat sender, string text)
    {
        if (string.IsNullOrWhiteSpace(text)) 
            throw new InvalidOperationException("The text cannot be null. ");

        Message_ID = Guid.NewGuid();
        Text = text;
        SendTime = DateTime.UtcNow; 
        Status = StatusMessage.Sent;
        Chat_ID = chat.Chat_ID;
        Sender_ID = sender.UserChat_ID;
    }

    public void MaskASDelivered()
    {
        if (Status == StatusMessage.Delivered || Status == StatusMessage.Sent)
            throw new ValidationException("The message cannot be change to delivered. ");
        if (Status == StatusMessage.Deleted)
            throw new ValidationException("The message is deleted. ");

        Status = StatusMessage.Delivered;
    }

    public void MaskAsRead()
    {
        if (Status == StatusMessage.Delivered || Status == StatusMessage.Read)
            throw new ValidationException("The message has already been deliverd. ");
        if (Status == StatusMessage.Deleted)
            throw new ValidationException("The message is deleted. ");

        Status = StatusMessage.Read;
    }
}
