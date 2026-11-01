using AppDeMensagem.Application.DTOs.Chat.Request;
using AppDeMensagem.Application.DTOs.Chat.Response;
using AppDeMensagem.Application.DTOs.ResponseApi;
using AppDeMensagem.Application.DTOs.UserChat.Response;
using AppDeMensagem.Application.UseCases;
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
    CreateChatGroupUseCase createChatGroupUseCase,
    SendMessageUseCase sendMessageUseCase,
    ListChatPrivateUseCase listChatPrivateUseCase,
    ListChatGroupUseCase listChatGroupUseCase,
    ListAllChatUseCase listAllChatUseCase,
    DeleteChatUseCase deleteChatUseCase,
    GetChatUseCase getChatUseCase
    ) : ControllerBase
{
    private Guid GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("Token inválido ou sem identificação de usuário. ");

        return Guid.Parse(claim);
    }

    [HttpPost("post/private")]
    [ProducesResponseType<SuccessResponse<string>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateChatPrivate([FromBody] RequestNewChat request)
    {
        Guid userPrimary_ID = GetCurrentUserId();
        var result = await createChatPrivateUseCase.ExecuteAsync(userPrimary_Id: userPrimary_ID, request: request);

        return Ok( new SuccessResponse<string>
        {
            Success = true,
            Data = result
        });
    }

    [HttpPost("post/group")]
    [ProducesResponseType<SuccessResponse<Guid>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateChatGroup([FromBody] RequestNewChatGroup request)
    {
        Guid userPrimary_ID = GetCurrentUserId();
        var result = await createChatGroupUseCase.ExecuteAsync(userPrimary_id: userPrimary_ID, request: request);

        return Ok(new SuccessResponse<Guid>
        {
            Success = true,
            Data = result
        });
    }

    [HttpPost("post/send-message")]
    [ProducesResponseType<SuccessResponse<ResponseSendMessage>>(StatusCodes.Status200OK)]
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

    [HttpGet("get")]
    [ProducesResponseType<SuccessResponse<ResponseGetChat>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChat([FromQuery] RequestGetChat request)
    {
        Guid userId = GetCurrentUserId();
        var result = await getChatUseCase.ExecuteAsync(request: request, userId);
        return Ok(new SuccessResponse<ResponseGetChat>
        {
            Success = true,
            Data = result
        });
    }

    [HttpGet("get/all")]
    [ProducesResponseType<SuccessResponse<List<ResponseChat>>>(StatusCodes.Status200OK)]
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
    [ProducesResponseType<SuccessResponse<List<ResponseChat>>>(StatusCodes.Status200OK)]
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
    [ProducesResponseType<SuccessResponse<List<ResponseChat>>>(StatusCodes.Status200OK)]
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

    [HttpDelete("delete/exit-chat/{chatId:guid}")]
    [ProducesResponseType<SuccessResponse<ResponseUserChat>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ExitChatGroup([FromRoute] Guid chatId)
    {
        var userId = GetCurrentUserId();

        var response = await deleteChatUseCase.ExecuteAsync(
            chatId: chatId, userId: userId);

        return Ok(new SuccessResponse<ResponseUserChat>
        {
            Success = true,
            Data = response
        });
    }
}
