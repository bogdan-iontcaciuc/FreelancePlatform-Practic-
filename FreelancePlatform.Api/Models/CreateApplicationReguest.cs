using System.ComponentModel.DataAnnotations;

public class CreateApplicationRequest
{
    [Required]
    public int AnuntId { get; set; }

    [Required]
    public string MesajAplicare { get; set; } = "";
}
