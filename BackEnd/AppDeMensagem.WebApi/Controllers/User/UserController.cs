
using AppDeMensagem.Application.DTOs.ResponseApi;
using AppDeMensagem.Application.DTOs.User;
using AppDeMensagem.Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace AppDeMensagem.WebApi.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserController(RegisterUseCase registerUseCase) : ControllerBase
{
    [HttpPost("post")]
    public async Task<IActionResult> RegisterUser(RequestRegister request)
    {
        var user = await registerUseCase.Execute(request);
        return Ok(new SuccessResponse<string>
        {
            Success = true,
            Data = user
        });
    }
    
}
