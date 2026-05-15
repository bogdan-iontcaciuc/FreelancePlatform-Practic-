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
        var anunt = await _context.Anunturi
            .FirstOrDefaultAsync(a => a.Id == request.AnuntId);

        if (anunt == null)
        {
            return (false, "Anunțul nu există.");
        }

        if (anunt.UtilizatorId == senderId)
        {
            return (false, "Nu poți aplica la propriul anunț.");
        }
        var message = new Message
        {
            Continut = request.Continut,
            ExpeditorId = senderId,
            DestinatarId = anunt.UtilizatorId,
            AnuntId = anunt.Id
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return (true, "Mesaj trimis.");
    }
    public async Task<List<InboxMessageDto>> GetInbox(int userId)
    {
        return await _context.Messages
            .Include(m => m.Expeditor)
            .Include(m => m.Anunt)
            .Where(m => m.DestinatarId == userId)
            .OrderByDescending(m => m.DataTrimiterii)
            .Select(m => new InboxMessageDto
            {
                Id = m.Id,
                Continut = m.Continut,
                Expeditor = m.Expeditor != null
                    ? m.Expeditor.NumeComplet
                    : "Utilizator necunoscut",
                TitluAnunt = m.Anunt != null
                    ? m.Anunt.Titlu
                    : "Anunț necunoscut",
                DataTrimiterii = m.DataTrimiterii
            })
            .ToListAsync();
    }
}