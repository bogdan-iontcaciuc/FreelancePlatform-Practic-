using System.Net.Http.Json;

public class ProfileService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;

    public ProfileService(
        HttpClient http,
        AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<ProfileModel?> GetProfile(int id)
    {
        return await _http.GetFromJsonAsync<ProfileModel>(
            $"api/profile/{id}");
    }

    public async Task<ProfileModel?> GetMyProfile()
    {
        await _authService.AddTokenToHeader();

        return await _http.GetFromJsonAsync<ProfileModel>(
            "api/profile/me");
    }

    public async Task<HttpResponseMessage>
        UpdateProfile(UpdateProfileModel model)
    {
        await _authService.AddTokenToHeader();

        return await _http.PutAsJsonAsync(
            "api/profile",
            model);
    }
}