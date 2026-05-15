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

    public DbSet<Message> Messages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Expeditor)
            .WithMany(u => u.MesajeTrimise)
            .HasForeignKey(m => m.ExpeditorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Destinatar)
            .WithMany(u => u.MesajePrimite)
            .HasForeignKey(m => m.DestinatarId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}