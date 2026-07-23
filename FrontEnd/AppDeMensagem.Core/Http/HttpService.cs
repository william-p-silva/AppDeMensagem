
using AppDeMensagem.Core.Model;
using System.Net.Http.Json;

namespace AppDeMensagem.Core.Http;

public class HttpService(HttpClient http)
{
    protected readonly HttpClient _http = http;

    public List<string> Error { get; set; } = new List<string>();

    public async Task<ResponseApi<TResponse?>> PostAsync<TResquest, TResponse>(string endpoint, TResquest? request)
    {
        Error.Clear();
        try
        {
            var response = await _http.PostAsJsonAsync(endpoint, request);

            if (!response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<ErrorResponseApi>();
                Error.Add(json?.Message ?? "Erro interno. ");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ResponseApi<TResponse>>();
        }
        catch (Exception ex)
        {
            Error.Add("Erro interno do servidor. ");
            return null;
        }
    }
}