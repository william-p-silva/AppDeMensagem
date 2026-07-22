
namespace AppDeMensagem.Core.Model;

public sealed record ErrorResponseApi
{
    public bool Success { get; set; }
    public string Message { get; set; }
}
