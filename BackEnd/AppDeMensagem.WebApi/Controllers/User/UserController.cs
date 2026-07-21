
using AppDeMensagem.Application.DTOs.ResponseApi;
using AppDeMensagem.Application.DTOs.User.Request;
using AppDeMensagem.Application.DTOs.User.Response;
using AppDeMensagem.Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace AppDeMensagem.WebApi.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserController(
    RegisterUseCase registerUseCase,
    LoginUseCase loginUseCase
    ) : ControllerBase
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

    [HttpPost("post/login")]
    public async Task<IActionResult> LoginUser(RequestLogin request)
    {
        var user = await loginUseCase.ExecuteAsync(request);
        return Ok(new SuccessResponse<ResponseLogin>
        {
            Success = true,
            Data = user
        });
    }
}
