

namespace AppDeMensagem.Application.DTOs.ResponseApi;

public sealed record ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}
