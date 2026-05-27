using FreelancePlatform.Api.Models;
using Microsoft.EntityFrameworkCore;

public class MessageService
{
    private readonly AppDbContext _context;

    public MessageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> SendMessage(
        int senderId,
        SendMessageRequest request)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);

        if (order == null)
        {
            return (false, "Order inexistent.");
        }

        bool isBuyer = order.BuyerId == senderId;
        bool isFreelancer = order.FreelancerId == senderId;
        if (!isBuyer && !isFreelancer)
        {
            return (false, "Nu ai acces la acest chat.");
        }

        int receiverId = isBuyer
            ? order.FreelancerId
            : order.BuyerId;

        var message = new Message
        {
            Continut = request.Continut,
            ExpeditorId = senderId,
            DestinatarId = receiverId,
            OrderId = request.OrderId,
            EsteLivrare = request.EsteLivrare,
            FisierUrl = request.FisierUrl
        };

        _context.Messages.Add(message);

        if (request.EsteLivrare)
        {
            order.Status = "Delivered";
            order.DeliveredAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return (true, "Mesaj trimis.");
    }

    public async Task<List<MessageDto>> GetMessages(
        int orderId,
        int userId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return new List<MessageDto>();
        }

        bool hasAccess =
            order.BuyerId == userId ||
            order.FreelancerId == userId;

        if (!hasAccess)
        {
            return new List<MessageDto>();
        }

        return await _context.Messages
            .Include(m => m.Expeditor)
            .Where(m => m.OrderId == orderId)
            .OrderBy(m => m.DataTrimiterii)
            .Select(m => new MessageDto
            {
                Id = m.Id,
                Continut = m.Continut,
                ExpeditorId = m.ExpeditorId,
                Expeditor = m.Expeditor != null
                    ? m.Expeditor.NumeComplet
                    : "Necunoscut",
                EsteLivrare = m.EsteLivrare,
                FisierUrl = m.FisierUrl,
                DataTrimiterii = m.DataTrimiterii,
                EsteEditat = m.EsteEditat,
                DataEditarii = m.DataEditarii
            })
            .ToListAsync();
    }
    public async Task<(bool Success, string Message)> EditMessage(
    int userId,
    EditMessageRequest request)
    {
        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.Id == request.MessageId);

        if (message == null)
        {
            return (false, "Mesaj inexistent.");
        }

        if (message.ExpeditorId != userId)
        {
            return (false, "Nu poți edita acest mesaj.");
        }

        message.Continut = request.ContinutNou;

        message.EsteEditat = true;

        message.DataEditarii = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return (true, "Mesaj editat.");
    }
}