using System.ComponentModel.DataAnnotations;

public class UpdateProfileRequest
{
    [Required]
    public string NumeComplet { get; set; } = "";

    public string Descriere { get; set; } = "";
}