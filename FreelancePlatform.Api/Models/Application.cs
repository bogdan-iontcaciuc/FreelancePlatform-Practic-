using FreelancePlatform.Api.Models;

public class Application
{
    public int Id { get; set; }

    public int AnuntId { get; set; }

    public Anunt? Anunt { get; set; }

    public int FreelancerId { get; set; }

    public User? Freelancer { get; set; }

    public string MesajAplicare { get; set; } = "";

    public string Status { get; set; } = "Pending";

    public DateTime DataAplicarii { get; set; }
        = DateTime.UtcNow;
}
