

namespace AppDeMensagem.Application.DTOs.ResponseApi;

public sealed record SuccessResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
}
