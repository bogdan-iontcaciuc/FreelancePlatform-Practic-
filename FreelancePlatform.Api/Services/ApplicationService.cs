using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

public class ApplicationService
{
    private readonly AppDbContext _context;

    public ApplicationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> Apply(
        int freelancerId,
        CreateApplicationRequest request)
    {
        var anunt = await _context.Anunturi
            .FirstOrDefaultAsync(a => a.Id == request.AnuntId);

        if (anunt == null)
        {
            return (false, "Anunțul nu există.");
        }
        if (anunt.UtilizatorId == freelancerId)
        {
            return (false, "Nu poți aplica la propriul anunț.");
        }

        var alreadyApplied = await _context.Applications
            .AnyAsync(a =>
                a.AnuntId == request.AnuntId &&
                a.FreelancerId == freelancerId);

        if (alreadyApplied)
        {
            return (false, "Ai aplicat deja.");
        }

        var application = new Application
        {
            AnuntId = request.AnuntId,
            FreelancerId = freelancerId,
            MesajAplicare = request.MesajAplicare
        };

        _context.Applications.Add(application);

        await _context.SaveChangesAsync();

        return (true, "Aplicație trimisă.");
    }
    public async Task<List<ApplicationDto>> GetApplicationsForAnunt(
        int anuntId)
    {
        return await _context.Applications
            .Include(a => a.Freelancer)
            .Where(a => a.AnuntId == anuntId)
            .Select(a => new ApplicationDto
            {
                Id = a.Id,
                Freelancer = a.Freelancer != null
                    ? a.Freelancer.NumeComplet
                    : "Necunoscut",
                MesajAplicare = a.MesajAplicare,
                Status = a.Status,
                DataAplicarii = a.DataAplicarii
            })
            .ToListAsync();
    }
}