using System.ComponentModel.DataAnnotations;

public class EditMessageRequest
{
    [Required]
    public int MessageId { get; set; }

    [Required]
    public string ContinutNou { get; set; } = string.Empty;
}
