using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<HttpResponseMessage> Register(string email, string numeComplet, string parola)
    {
        Console.WriteLine("Hello !!");
        var request = new
        {
            email,
            numeComplet,
            parola
        };

        return await _http.PostAsJsonAsync("/inregistrare", request);
    }
}