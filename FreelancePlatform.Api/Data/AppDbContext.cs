using Microsoft.EntityFrameworkCore;
using FreelancePlatform.Api.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Anunt> Anunturi { get; set; }
}