using System.ComponentModel.DataAnnotations;

public class InregistrareRequest
{
    [Required(ErrorMessage = "Email-ul este obligatoriu")]
    [EmailAddress(ErrorMessage = "Email invalid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Numele complet este obligatoriu")]
    public string NumeComplet { get; set; }

    [Required(ErrorMessage = "Parola este obligatorie")]
    [MinLength(6, ErrorMessage = "Parola trebuie să aibă minim 6 caractere")]
    public string Parola { get; set; }
}