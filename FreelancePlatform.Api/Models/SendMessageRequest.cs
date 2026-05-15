using System.ComponentModel.DataAnnotations;

public class SendMessageRequest
{
    [Required]
    public int AnuntId { get; set; }

    [Required]
    public string Continut { get; set; } = string.Empty;
}