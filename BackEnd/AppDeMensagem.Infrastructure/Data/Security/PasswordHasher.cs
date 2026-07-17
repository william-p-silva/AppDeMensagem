
using AppDeMensagem.Application.Interfaces.Security;

namespace AppDeMensagem.Infrastructure.Data.Security;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("The password cannot be null or void. ");

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHasher)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHasher);
    }
}
