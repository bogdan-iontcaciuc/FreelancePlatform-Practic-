using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Parolele nu coincid")]
        public string ConfirmPassword { get; set; }
    }
}