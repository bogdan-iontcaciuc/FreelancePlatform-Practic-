using FreelancePlatform.Api.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Anunt> Anunturi { get; set; }

    public DbSet<Message> Messages { get; set; }
    public DbSet<Application> Applications { get; set; }

    public DbSet<Order> Orders { get; set; }
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
        modelBuilder.Entity<Application>()
    .HasOne(a => a.Freelancer)
    .WithMany(u => u.Aplicatii)
    .HasForeignKey(a => a.FreelancerId)
    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Buyer)
            .WithMany(u => u.OrdersAsBuyer)
            .HasForeignKey(o => o.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Freelancer)
            .WithMany(u => u.OrdersAsFreelancer)
            .HasForeignKey(o => o.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}