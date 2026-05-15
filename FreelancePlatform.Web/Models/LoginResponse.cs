namespace FreelancePlatform.Web.Models;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NumeComplet { get; set; } = string.Empty;
}