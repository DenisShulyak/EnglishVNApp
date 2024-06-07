using System.Net.Http.Json;

namespace MauiApp1.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    public async Task<T> GetAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>();
    }
}