
using FreelancePlatform.Api.Models;
using static System.Net.Mime.MediaTypeNames;

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
    public ICollection<Application> Aplicatii { get; set; }
    = new List<Application>();

    public ICollection<Order> OrdersAsBuyer { get; set; }
        = new List<Order>();

    public ICollection<Order> OrdersAsFreelancer { get; set; }
        = new List<Order>();
}