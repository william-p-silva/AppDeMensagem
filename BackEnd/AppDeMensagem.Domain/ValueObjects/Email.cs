

using System.ComponentModel.DataAnnotations;

namespace AppDeMensagem.Domain.ValueObjects;

public sealed record Email
{
    public string Endereco { get; }

    private Email(string endereco) { Endereco = endereco; }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email Inválido. ");

        EmailAddressAttribute validator = new();
        if (!validator.IsValid(email)) throw new ArgumentException("Email Inválido");

        return new Email(email);
    } 
}
