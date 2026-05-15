using System.Net.Http.Json;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public AuthService(
        HttpClient http,
        ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<HttpResponseMessage> Register(
        string email,
        string numeComplet,
        string parola)
    {
        var request = new
        {
            email,
            numeComplet,
            parola
        };

        return await _http.PostAsJsonAsync(
            "api/Auth/inregistrare",
            request
        );
    }

    public async Task<HttpResponseMessage> Login(
        string email,
        string parola)
    {
        var request = new
        {
            email,
            parola
        };

        return await _http.PostAsJsonAsync(
            "api/Auth/logare",
            request
        );
    }

    public async Task SaveToken(string token)
    {
        await _localStorage.SetItemAsync(
            "authToken",
            token);
    }

    public async Task<string?> GetToken()
    {
        return await _localStorage.GetItemAsync<string>(
            "authToken");
    }

    public async Task AddTokenToHeader()
    {
        var token = await GetToken();

        if (!string.IsNullOrWhiteSpace(token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(
            "authToken");

        _http.DefaultRequestHeaders.Authorization = null;
    }
}