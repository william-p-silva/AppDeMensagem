using AppDeMensagem.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppDeMensagem.WebApi.Controllers;

[ApiController]
[Route("teste")]
public class TesteController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Teste()
    {
        Usuario user = new Usuario("email@gami.com", "nome1 nome", "senha", Domain.Enum.PerfilUser.User);
        return Ok(user);
    }
}
