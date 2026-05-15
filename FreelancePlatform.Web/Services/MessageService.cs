using System.Net.Http.Json;

public class MessageService
{
    private readonly HttpClient _http;

    private readonly AuthService _authService;

    public MessageService(
        HttpClient http,
        AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<HttpResponseMessage> Send(
        SendMessageModel model)
    {
        await _authService.AddTokenToHeader();
        return await _http.PostAsJsonAsync(
            "api/messages",
            model
        );
    }

    public async Task<List<MessageModel>> GetInbox()
    {
        await _authService.AddTokenToHeader();
        return await _http.GetFromJsonAsync<
            List<MessageModel>>(
            "api/messages/inbox"
        ) ?? new();
    }
}