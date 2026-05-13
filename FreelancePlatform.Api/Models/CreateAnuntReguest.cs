using System.ComponentModel.DataAnnotations;

public class CreateAnuntRequest
{
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

    [Required]
    public int UtilizatorId { get; set; }

    public string Status { get; set; } = "Activ";
}