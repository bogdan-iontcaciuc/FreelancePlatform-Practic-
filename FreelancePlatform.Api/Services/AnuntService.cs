using Microsoft.EntityFrameworkCore;
using FreelancePlatform.Api.Models;

public class AnuntService
{
    private readonly AppDbContext _context;

    public AnuntService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> CreateAnunt(
        CreateAnuntRequest request)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.Id == request.UtilizatorId);

        if (!userExists)
        {
            return (false, "Utilizatorul nu există.");
        }
        var anunt = new Anunt
        {
            Titlu = request.Titlu,
            Descriere = request.Descriere,
            TipAnunt = request.TipAnunt,
            Categorie = request.Categorie,
            Tehnologii = request.Tehnologii,
            PretSauBuget = request.PretSauBuget,
            UtilizatorId = request.UtilizatorId,
            DataPublicarii = DateTime.UtcNow
        };

        _context.Anunturi.Add(anunt);

        await _context.SaveChangesAsync();

        return (true, "Anunț creat cu succes!");
    }
    public async Task<List<Anunt>> GetActiveAnunturi()
    {
        return await _context.Anunturi
            .Where(a => a.Status == "Activ")
            .OrderByDescending(a => a.DataPublicarii)
            .ToListAsync();
    }
}