using Microsoft.EntityFrameworkCore;

public class ProfileService
{
    private readonly AppDbContext _context;

    public ProfileService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProfileResponse?> GetProfile(int userId)
    {
        var user = await _context.Users
            .Include(u => u.Anunturi)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        var completedOrders = await _context.Orders
            .Include(o => o.Anunt)
            .Where(o =>
                o.FreelancerId == userId &&
                o.Status == "Completed")
            .ToListAsync();

        var totalAplicatii = await _context.Applications
            .CountAsync(a => a.FreelancerId == userId);

        return new ProfileResponse
        {
            Id = user.Id,

            NumeComplet = user.NumeComplet,

            Email = user.Email,

            Descriere = user.Descriere,

            TotalAnunturi = user.Anunturi.Count,

            TotalAplicatii = totalAplicatii,

            TotalProiecteFinalizate = completedOrders.Count,

            Anunturi = user.Anunturi
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
                    NumeUtilizator = user.NumeComplet
                })
                .ToList(),

            ProiecteFinalizate = completedOrders
                .Select(o => new OrderDto
                {
                    Id = o.Id,

                    AnuntId = o.AnuntId,

                    TitluAnunt = o.Anunt != null
                        ? o.Anunt.Titlu
                        : "",

                    Categorie = o.Anunt != null
                        ? o.Anunt.Categorie
                        : "",

                    TipAnunt = o.Anunt != null
                        ? o.Anunt.TipAnunt
                        : "",

                    PretSauBuget = o.Anunt != null
                        ? o.Anunt.PretSauBuget
                        : 0,

                    Status = o.Status,

                    CreatedAt = o.CreatedAt
                })
                .ToList()
        };
    }

    public async Task<(bool Success, string Message)>
        UpdateProfile(
        int userId,
        UpdateProfileRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return (false, "Utilizator inexistent.");
        }

        user.NumeComplet = request.NumeComplet;
        user.Descriere = request.Descriere;

        await _context.SaveChangesAsync();

        return (true, "Profil actualizat.");
    }
}