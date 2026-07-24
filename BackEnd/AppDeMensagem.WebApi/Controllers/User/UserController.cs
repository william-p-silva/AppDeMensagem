
using AppDeMensagem.Application.DTOs.ResponseApi;
using AppDeMensagem.Application.DTOs.User.Request;
using AppDeMensagem.Application.DTOs.User.Response;
using AppDeMensagem.Application.UseCases.User;
using AppDeMensagem.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        var cookiesOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(3)
        };

        Response.Cookies.Append("AuthToken", user.Token, cookiesOptions);

        return Ok(new SuccessResponse<Object>
        {
            Success = true,
            Data = new 
            {
                Name = user.Name,
                Email = user.Email,
                Profile = user.Profile,
            }
        });
    }


    [HttpPost("me")]
    [Authorize]
    public IActionResult Me()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var profile = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new SuccessResponse<Object>
        {
            Success = true,
            Data = new
            {
                Name = name,
                Email = email,
                Profile = profile,
            }
        });
    }

    [HttpPost("logout")] 
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return Ok(new SuccessResponse<string>
        {
            Success = true,
            Data = "Logout realizado com sucesso"
        });
    }
}
