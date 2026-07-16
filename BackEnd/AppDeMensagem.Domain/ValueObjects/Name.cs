

using System.Text.RegularExpressions;

namespace AppDeMensagem.Domain.ValueObjects;

public sealed record Name
{
    private static readonly string padrao = @"^[a-zA-ZÀ-ÿ\s'-]+$";
    
    public string TextName { get; }

    private Name(string textName) {  TextName = textName; }

    public static Name Create(string textName)
    {
        if (string.IsNullOrWhiteSpace(textName) || textName.Count() >= 151)
            throw new ArgumentNullException("The name cannot be null. ", nameof(textName));

        if(!Regex.IsMatch(textName, padrao))
            throw new ArgumentException("The name cannot be null. ", nameof(textName));
        return new Name(textName);
    }
}
