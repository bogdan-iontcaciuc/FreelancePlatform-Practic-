using System.Net.Http.Json;
using static FreelancePlatform.Web.Components.Pages.Home;

public class AnuntService
{
    private readonly HttpClient _http;

    public AnuntService(HttpClient http)
    {
        _http = http;
    }

    public async Task<HttpResponseMessage> Create(
        CreateAnuntModel model)
    {
        return await _http.PostAsJsonAsync(
            "api/anunturi",
            model
        );
    }
    public async Task<List<Anunt>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<Anunt>>(
            "api/anunturi"
        ) ?? new();
    }
}