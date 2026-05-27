using System.Net.Http.Json;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public event Action? OnAuthStateChanged;

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
            token
        );

        OnAuthStateChanged?.Invoke(); // refresh navbar instant
    }

    public async Task<string?> GetToken()
    {
        try
        {
            return await _localStorage.GetItemAsync<string>(
                "authToken"
            );
        }
        catch
        {
            return null;
        }
    }

    public async Task AddTokenToHeader()
    {
        var token = await GetToken();

        if (!string.IsNullOrWhiteSpace(token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token
                );
        }
    }
    public async Task<int> GetUserId()
    {
        var token = await GetToken();

        if (string.IsNullOrWhiteSpace(token))
        {
            return 0;
        }

        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        var userIdClaim = jwt.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier ||
            c.Type == "nameid");

        if (userIdClaim == null)
        {
            return 0;
        }

        return int.Parse(userIdClaim.Value);
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(
            "authToken"
        );

        _http.DefaultRequestHeaders.Authorization = null;

        OnAuthStateChanged?.Invoke(); // refresh navbar instant
    }

}