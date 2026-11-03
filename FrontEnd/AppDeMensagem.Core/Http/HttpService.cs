
using AppDeMensagem.Core.Model;
using System.Net.Http.Json;

namespace AppDeMensagem.Core.Http;

public class HttpService(HttpClient http)
{
    protected readonly HttpClient _http = http;

    public List<string> Error { get; set; } = new List<string>();

    private async Task<ResponseApi<TResponse?>> HandleResponse<TResponse>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            await HandleError(response);
        }

        var result = await response.Content
            .ReadFromJsonAsync<ResponseApi<TResponse>>();

        return result ?? throw new HttpError("Resposta inválida da API.");
    }

    private async Task HandleError(HttpResponseMessage response)
    {
        var error = await response.Content
            .ReadFromJsonAsync<ErrorResponseApi>();

        var message = error?.Message ?? "Erro interno.";

        Error.Add(message);

        throw new HttpError(message);
    }

    public async Task<ResponseApi<TResponse?>> PostAsync<TResquest, TResponse>(string endpoint, TResquest? request)
    {
        Error.Clear();
        using var response = await _http.PostAsJsonAsync(endpoint, request);
        return await HandleResponse<TResponse>(response);
    }

    public async Task<ResponseApi<TResponse?>> GetAsync<TResponse>(string endpoint)
    {
        Error.Clear();
        using var response = await _http.GetAsync(endpoint);
        return await HandleResponse<TResponse>(response);
    }

    public async Task<ResponseApi<TResponse?>> PutAsync<TRequest, TResponse>(string endpoint, TRequest? request)
    {
        Error.Clear();
        using var response = await _http.PutAsJsonAsync(endpoint, request);
        return await HandleResponse<TResponse>(response);
    }

    public async Task<ResponseApi<TResponse?>> DeleteAsync<TResponse>(string endpoint)
    {
        Error.Clear();
        using var response = await _http.DeleteAsync(endpoint);
        return await HandleResponse<TResponse>(response);
    }
}