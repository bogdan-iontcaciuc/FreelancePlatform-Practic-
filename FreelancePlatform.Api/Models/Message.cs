using System.ComponentModel.DataAnnotations;

namespace FreelancePlatform.Api.Models;

public class Message
{
    public int Id { get; set; }

    [Required]
    public string Continut { get; set; } = string.Empty;

    [Required]
    public int ExpeditorId { get; set; }

    public User? Expeditor { get; set; }

    [Required]
    public int DestinatarId { get; set; }

    public User? Destinatar { get; set; }

    public int? OrderId { get; set; }

    public Order? Order { get; set; }

    public bool EsteLivrare { get; set; } = false;

    public string? FisierUrl { get; set; }

    public DateTime DataTrimiterii { get; set; }
        = DateTime.UtcNow;
}