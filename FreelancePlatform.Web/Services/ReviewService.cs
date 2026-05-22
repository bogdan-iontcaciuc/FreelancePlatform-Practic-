public class ReviewService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;

    public ReviewService(
        HttpClient http,
        AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<HttpResponseMessage>
        Create(CreateReviewModel model)
    {
        await _authService.AddTokenToHeader();

        return await _http.PostAsJsonAsync(
            "api/reviews",
            model);
    }
}