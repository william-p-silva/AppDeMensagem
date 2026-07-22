

using AppDeMensagem.Shared.Header.Models;

namespace AppDeMensagem.Shared.Header.Services;

public class NavServices
{
    public List<LinksHeader> ListLinks { get; set; } = new List<LinksHeader>()
    {
        new LinksHeader("/login", "Login"),
        new LinksHeader("/cadastro", "Cadastro"),
        new LinksHeader("/teste", "Teste")
    };
}


