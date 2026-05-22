using System.ComponentModel.DataAnnotations;

public class UpdateAnuntModel
{
    [Required]
    public string Titlu { get; set; } = "";

    [Required]
    public string Descriere { get; set; } = "";

    [Required]
    public string TipAnunt { get; set; } = "";

    [Required]
    public string Categorie { get; set; } = "";

    public string Tehnologii { get; set; } = "";

    [Required]
    [Range(1, 1000000)]
    public decimal PretSauBuget { get; set; }
}