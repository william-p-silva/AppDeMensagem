
namespace AppDeMensagem.Core.Http;

public class HttpError : Exception
{
    public HttpError(string? message) : base(message)
    {
    }
}
