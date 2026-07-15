
using AppDeMensagem.Domain.Enum;
using AppDeMensagem.Domain.ValueObjects;

namespace AppDeMensagem.Domain.Entity;

public class Usuario
{
    public Guid User_ID { get; private set; }
    public Email EmailAddress { get; private set; }
    public Name UserName { get; private set; }
    public string PasswordHash { get; private set; }
    public PerfilUser UserProfile { get; private set; }

    private readonly List<UserChat> _usersChat = new List<UserChat>();

    public IReadOnlyCollection<UserChat> UsersChat => _usersChat.AsReadOnly();


    protected Usuario() { }
    public Usuario(
        string email, string userName, string passwordHash, PerfilUser userProfile)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentNullException("The password cannot be null. ", nameof(passwordHash));

        User_ID = Guid.NewGuid();
        EmailAddress = Email.Create(email);
        UserName = Name.Create(userName);
        PasswordHash = passwordHash;
        UserProfile = userProfile;
    }
}
