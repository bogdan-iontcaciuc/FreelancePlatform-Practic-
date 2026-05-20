using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace FreelancePlatform.Api.Models;

public class Anunt
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Titlu { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Descriere { get; set; } = string.Empty;

    [Required]
    public string TipAnunt { get; set; } = string.Empty;

    [Required]
    public string Categorie { get; set; } = string.Empty;

    public string Tehnologii { get; set; } = string.Empty;

    [Range(0, 1000000)]
    public decimal PretSauBuget { get; set; }

    public DateTime DataPublicarii { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Activ";

    [Required]
    public int UtilizatorId { get; set; }

    public User? Utilizator { get; set; }
    public ICollection<Message> Mesaje { get; set; }
    = new List<Message>();
    public ICollection<Application> Aplicatii { get; set; }
    = new List<Application>();

    public ICollection<Order> Orders { get; set; }
        = new List<Order>();
}