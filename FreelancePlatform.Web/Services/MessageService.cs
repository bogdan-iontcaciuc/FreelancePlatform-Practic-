using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
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

    public async Task<List<MessageDto>>
        GetMessages(int orderId)
    {
        await _authService.AddTokenToHeader();

        return await _http.GetFromJsonAsync<
            List<MessageDto>>(
            $"api/messages/order/{orderId}"
        ) ?? new();
    }
    public async Task<string> UploadFile(IBrowserFile file)
    {
        await _authService.AddTokenToHeader();

        var content = new MultipartFormDataContent();

        var stream = file.OpenReadStream(1024 * 1024 * 50);

        var fileContent = new StreamContent(stream);

        fileContent.Headers.ContentType =
            new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

        content.Add(fileContent, "file", file.Name);

        var response = await _http.PostAsync(
            "api/files/upload",
            content
        );

        if (!response.IsSuccessStatusCode)
        {
            return "";
        }

        var result =
            await response.Content
                .ReadFromJsonAsync<FileUploadResponse>();

        return result?.Url ?? "";
    }
}