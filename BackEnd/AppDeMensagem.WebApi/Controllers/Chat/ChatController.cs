using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.DTOs.Chat.Response;
using AppDeMensagem.Application.DTOs.ResponseApi;
using AppDeMensagem.Application.UseCases.Chat;
using AppDeMensagem.Application.UseCases.Chat.Create;
using AppDeMensagem.Application.UseCases.Chat.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppDeMensagem.WebApi.Controllers.Chat;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ChatController(
    CreateChatPrivateUseCase createChatPrivateUseCase,
    SendMessageUseCase sendMessageUseCase,
    ListChatPrivateUseCase listChatPrivateUseCase,
    ListChatGroupUseCase listChatGroupUseCase,
    ListAllChatUseCase listAllChatUseCase
    ) : ControllerBase
{
    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("Token inválido ou sem identificação de usuário. ");

        return Guid.Parse(claim);
    }

    [HttpPost("post/private")]
    public async Task<IActionResult> CreateChatPrivate([FromBody] Guid userSecond_id)
    {
        Guid userPrimary_ID = GetCurrentUserId();
        var result = await createChatPrivateUseCase.ExecuteAsync(userPrimary_Id: userPrimary_ID, userSecond_Id: userSecond_id);

        return Ok( new SuccessResponse<string>
        {
            Success = true,
            Data = result
        });
    }

    [HttpPost("post/send-message")]
    public async Task<IActionResult> SendMessage([FromBody] RequestSendMessage request)
    {
        Guid userId = GetCurrentUserId();

        var result = await sendMessageUseCase.ExecuteAsync(request: request, userId: userId);

        return Ok(new SuccessResponse<ResponseSendMessage>
        {
            Success = true,
            Data = result
        });
    }

    [HttpGet("get/all")]
    public async Task<IActionResult> ListAll()
    {
        Guid userId = GetCurrentUserId();

        var chats = await listAllChatUseCase.ExecuteAsync(userId);

        return Ok(new SuccessResponse<List<ResponseChat>>
        {
            Success = true,
            Data = chats
        });
    }

    [HttpGet("get/private")]
    public async Task<IActionResult> ListPrivate()
    {
        Guid userId = GetCurrentUserId();

        var chats = await listChatPrivateUseCase.ExecuteAsync(userId);

        return Ok(new SuccessResponse<List<ResponseChat>>
        {
            Success = true,
            Data = chats
        });
    }

    [HttpGet("get/group")]
    public async Task<IActionResult> ListGroup()
    {
        Guid userId = GetCurrentUserId();

        var chats = await listChatGroupUseCase.ExecuteAsync(userId);

        return Ok(new SuccessResponse<List<ResponseChat>>
        {
            Success = true,
            Data = chats
        });
    }
}
