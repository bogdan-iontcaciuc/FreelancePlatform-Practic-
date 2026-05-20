using System.ComponentModel.DataAnnotations;

public class SendMessageRequest
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public string Continut { get; set; } = string.Empty;

    public bool EsteLivrare { get; set; }

    public string? FisierUrl { get; set; }
}