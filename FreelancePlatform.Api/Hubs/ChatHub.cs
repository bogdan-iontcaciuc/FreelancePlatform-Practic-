using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    // 1. JOIN GROUP SAFE
    public async Task JoinOrderGroup(int orderId)
    {
        if (orderId <= 0)
            return;

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"order-{orderId}");
    }

    // 2. LEAVE GROUP SAFE
    public async Task LeaveOrderGroup(int orderId)
    {
        if (orderId <= 0)
            return;

        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            $"order-{orderId}");
    }

    // 3. SEND MESSAGE BROADCAST TEST (SAFE)
    public async Task SendMessageTest(int orderId, string message)
    {
        if (orderId <= 0 || string.IsNullOrWhiteSpace(message))
            return;

        await Clients.Group($"order-{orderId}")
            .SendAsync("ReceiveMessage", new
            {
                Expeditor = "TEST",
                Continut = message,
                DataTrimiterii = DateTime.Now
            });
    }

    // 4. TYPING SAFE
    public async Task Typing(int orderId)
    {
        if (orderId <= 0) return;

        await Clients.OthersInGroup($"order-{orderId}")
            .SendAsync("UserTyping", "User");
    }

    // 5. STOP TYPING SAFE
    public async Task StopTyping(int orderId)
    {
        if (orderId <= 0) return;

        await Clients.OthersInGroup($"order-{orderId}")
            .SendAsync("StopTyping");
    }

    // 6. SEEN SAFE
    public async Task MarkSeen(int orderId)
    {
        if (orderId <= 0) return;

        await Clients.OthersInGroup($"order-{orderId}")
            .SendAsync("MessagesSeen", orderId);
    }
}