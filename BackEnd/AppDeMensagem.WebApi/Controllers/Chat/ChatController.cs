using AppDeMensagem.Application.DTOs.Chat.Response;
using AppDeMensagem.Application.DTOs.ResponseApi;
using AppDeMensagem.Application.UseCases.Chat.Create;
using AppDeMensagem.Application.UseCases.Chat.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppDeMensagem.WebApi.Controllers.Chat;

[ApiController]
[Route("[controller]")]
public class ChatController(
    CreateChatPrivateUseCase createChatPrivateUseCase,
    ListChatPrivateUseCase listChatPrivateUseCase,
    ListChatGroupUseCase listChatGroupUseCase,
    ListAllChatUseCase listAllChatUseCase
    ) : ControllerBase
{

    [HttpPost("post/private")]
    [Authorize]
    public async Task<IActionResult> CreateChatPrivate(Guid userSecond_id)
    {
        var userPrimary_ID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await createChatPrivateUseCase.ExecuteAsync(userPrimary_Id: userPrimary_ID, userSecond_Id: userSecond_id);

        return Ok( new SuccessResponse<string>
        {
            Success = true,
            Data = result
        });
    }

    [HttpGet("get/all")]
    [Authorize]
    public async Task<IActionResult> ListAll()
    {
        var chats = await listAllChatUseCase.ExecuteAsync();

        return Ok(new SuccessResponse<List<ResponseChat>>
        {
            Success = true,
            Data = chats
        });
    }

    [HttpGet("get/private")]
    [Authorize]
    public async Task<IActionResult> ListPrivate()
    {
        var chats = await listChatPrivateUseCase.ExecuteAsync();

        return Ok(new SuccessResponse<List<ResponseChat>>
        {
            Success = true,
            Data = chats
        });
    }

    [HttpGet("get/group")]
    [Authorize]
    public async Task<IActionResult> ListGroup()
    {
        var chats = await listChatGroupUseCase.ExecuteAsync();

        return Ok(new SuccessResponse<List<ResponseChat>>
        {
            Success = true,
            Data = chats
        });
    }
}
