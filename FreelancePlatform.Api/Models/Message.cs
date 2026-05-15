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

    [Required]
    public int AnuntId { get; set; }

    public Anunt? Anunt { get; set; }

    public DateTime DataTrimiterii { get; set; }
        = DateTime.UtcNow;
}