using System.Net.Http.Json;

public class AnuntService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;
    public AnuntService(
    HttpClient http,
    AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<HttpResponseMessage> Create(
        CreateAnuntModel model)
    {
        await _authService.AddTokenToHeader();

        return await _http.PostAsJsonAsync(
            "api/anunturi",
            model
        );
    }

    public async Task<List<AnuntModel>> GetAll()
    {
        return await _http.GetFromJsonAsync<
            List<AnuntModel>>(
            "api/anunturi"
        ) ?? new();
    }

    public async Task<AnuntModel?> GetById(int id)
    {
        return await _http.GetFromJsonAsync<
            AnuntModel>(
            $"api/anunturi/{id}"
        );
    }
    public async Task<List<AnuntModel>> GetMine()
    {
        await _authService.AddTokenToHeader();

        return await _http.GetFromJsonAsync<List<AnuntModel>>(
            "api/anunturi/my"
        ) ?? new();
    }
    public async Task<HttpResponseMessage> Update(
    int id,
    UpdateAnuntModel model)
    {
        await _authService.AddTokenToHeader();

        return await _http.PutAsJsonAsync(
            $"api/anunturi/{id}",
            model);
    }

    public async Task<HttpResponseMessage> Delete(int id)
    {
        await _authService.AddTokenToHeader();

        return await _http.DeleteAsync(
            $"api/anunturi/{id}");
    }
}