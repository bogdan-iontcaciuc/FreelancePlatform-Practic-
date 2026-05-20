using System.Net.Http.Json;

public class OrderService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;

    public OrderService(
        HttpClient http,
        AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<HttpResponseMessage>
        CreateFromApplication(int applicationId)
    {
        await _authService.AddTokenToHeader();

        return await _http.PostAsync(
            $"api/orders/create-from-application/{applicationId}",
            null
        );
    }

    public async Task<List<OrderModel>> GetOrders()
    {
        await _authService.AddTokenToHeader();


var response = await _http.GetAsync("api/orders");

        if (!response.IsSuccessStatusCode)
        {
            return new List<OrderModel>();
        }

        return await response.Content
            .ReadFromJsonAsync<List<OrderModel>>()
            ?? new List<OrderModel>();


}


    public async Task<HttpResponseMessage>
        CompleteOrder(int orderId)
    {
        await _authService.AddTokenToHeader();

        return await _http.PostAsync(
            $"api/orders/complete/{orderId}",
            null
        );
    }
}