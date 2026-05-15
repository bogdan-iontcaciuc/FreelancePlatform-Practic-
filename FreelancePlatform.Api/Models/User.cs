
using FreelancePlatform.Api.Models;

public class User

{
    public int Id { get; set; }
    public string Email { get; set; }
    public string NumeComplet { get; set; }
    public string Parola { get; set; }
    public ICollection<Anunt> Anunturi { get; set; } = new List<Anunt>();
    public ICollection<Message> MesajeTrimise { get; set; }
    = new List<Message>();

    public ICollection<Message> MesajePrimite { get; set; }
        = new List<Message>();
}