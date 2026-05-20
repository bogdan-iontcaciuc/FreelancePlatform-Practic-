using System.Net.Http.Json;

public class ApplicationService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;

    public ApplicationService(
        HttpClient http,
        AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<HttpResponseMessage> Apply(
        int anuntId,
        string mesaj)
    {
        await _authService.AddTokenToHeader();

        return await _http.PostAsJsonAsync(
            "api/applications",
            new
            {
                AnuntId = anuntId,
                MesajAplicare = mesaj
            });
    }

    public async Task<List<ApplicationModel>>
        GetApplications(int anuntId)
    {
        await _authService.AddTokenToHeader();

        return await _http.GetFromJsonAsync<
            List<ApplicationModel>>(
            $"api/applications/anunt/{anuntId}"
        ) ?? new();
    }
}