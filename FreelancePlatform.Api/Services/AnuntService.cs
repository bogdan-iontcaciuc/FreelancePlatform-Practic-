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
    CreateAnuntRequest request,
    int userId)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.Id == userId);

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
            UtilizatorId = userId,   // IMPORTANT
            DataPublicarii = DateTime.UtcNow
        };

        _context.Anunturi.Add(anunt);

        await _context.SaveChangesAsync();

        return (true, "Anunț creat cu succes!");
    }
    public async Task<List<AnuntResponse>> GetActiveAnunturi()
    {
        return await _context.Anunturi
            .Include(a => a.Utilizator)
            .Where(a => a.Status == "Activ")
            .Select(a => new AnuntResponse
            {
                Id = a.Id,
                Titlu = a.Titlu,
                Descriere = a.Descriere,
                Categorie = a.Categorie,
                TipAnunt = a.TipAnunt,
                Tehnologii = a.Tehnologii,
                PretSauBuget = a.PretSauBuget,
                DataPublicarii = a.DataPublicarii,
                UtilizatorId = a.UtilizatorId,
                NumeUtilizator = a.Utilizator != null
                    ? a.Utilizator.NumeComplet
                    : ""
            })
            .ToListAsync();
    }
    public async Task<AnuntResponse?> GetById(int id)
    {
        return await _context.Anunturi
            .Include(a => a.Utilizator)
            .Where(a => a.Id == id)
            .Select(a => new AnuntResponse
            {
                Id = a.Id,
                Titlu = a.Titlu,
                Descriere = a.Descriere,
                Categorie = a.Categorie,
                TipAnunt = a.TipAnunt,
                Tehnologii = a.Tehnologii,
                PretSauBuget = a.PretSauBuget,
                DataPublicarii = a.DataPublicarii,
                UtilizatorId = a.UtilizatorId,
                NumeUtilizator = a.Utilizator != null
                    ? a.Utilizator.NumeComplet
                    : ""
            })
            .FirstOrDefaultAsync();
    }
    public async Task<(bool Success, string Message)> UpdateAnunt(
    int id,
    int userId,
    UpdateAnuntRequest request)
    {
        var anunt = await _context.Anunturi
            .FirstOrDefaultAsync(a => a.Id == id);

        if (anunt == null)
        {
            return (false, "Anunțul nu există.");
        }

        if (anunt.UtilizatorId != userId)
        {
            return (false, "Nu ai acces.");
        }

        anunt.Titlu = request.Titlu;
        anunt.Descriere = request.Descriere;
        anunt.Categorie = request.Categorie;
        anunt.TipAnunt = request.TipAnunt;
        anunt.Tehnologii = request.Tehnologii;
        anunt.PretSauBuget = request.PretSauBuget;

        await _context.SaveChangesAsync();

        return (true, "Anunț actualizat.");
    }

    public async Task<(bool Success, string Message)> DeleteAnunt(
        int id,
        int userId)
    {
        var anunt = await _context.Anunturi
            .Include(a => a.Aplicatii)
            .Include(a => a.Orders)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (anunt == null)
        {
            return (false, "Anunțul nu există.");
        }

        if (anunt.UtilizatorId != userId)
        {
            return (false, "Nu ai acces.");
        }

        if (anunt.Orders.Any())
        {
            return (false, "Nu poți șterge un anunț cu order activ.");
        }

        _context.Applications.RemoveRange(anunt.Aplicatii);

        _context.Anunturi.Remove(anunt);

        await _context.SaveChangesAsync();

        return (true, "Anunț șters.");
    }
}