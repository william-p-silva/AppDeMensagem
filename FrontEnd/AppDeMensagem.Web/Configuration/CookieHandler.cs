using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace AppDeMensagem.Web.Configuration;

public class CookieHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
    {
        // Força o Fetch do navegador a enviar os cookies armazenados para esse domínio
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        return await base.SendAsync(request, cancellationToken);
    }
}
