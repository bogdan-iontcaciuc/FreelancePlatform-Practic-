using System.ComponentModel.DataAnnotations;

public class CreateAnuntModel
{
    [Required]
    public string Titlu { get; set; } = string.Empty;

    [Required]
    public string Descriere { get; set; } = string.Empty;

    [Required]
    public string TipAnunt { get; set; } = string.Empty;

    [Required]
    public string Categorie { get; set; } = string.Empty;

    public string Tehnologii { get; set; } = string.Empty;

    [Required(ErrorMessage = "Prețul/Bugetul este obligatoriu")]
    [Range(1, 1000000,
    ErrorMessage = "Prețul/Bugetul trebuie să fie mai mare decât 0")]
    public decimal PretSauBuget { get; set; }

}