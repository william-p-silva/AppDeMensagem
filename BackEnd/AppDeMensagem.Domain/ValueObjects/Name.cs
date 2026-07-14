

namespace AppDeMensagem.Domain.ValueObjects;

public sealed record Name
{
    public string TextName { get; }

    private Name(string textName) {  TextName = textName; }

    public static Name Create(string textName)
    {
        if (string.IsNullOrWhiteSpace(textName))
            throw new ArgumentNullException("The name cannot be null. ", nameof(textName));

        bool isLetters = textName.All(char.IsLetter);
        if (!isLetters) throw new ArgumentException("The name is invalid. ", nameof(textName));

        return new Name(textName);
    }
}
