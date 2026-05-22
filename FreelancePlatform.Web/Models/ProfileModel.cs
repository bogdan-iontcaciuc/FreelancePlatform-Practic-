public class ProfileModel
{
    public int Id { get; set; }

    public string NumeComplet { get; set; } = "";

    public string Email { get; set; } = "";

    public string Descriere { get; set; } = "";

    public int TotalAnunturi { get; set; }

    public int TotalAplicatii { get; set; }

    public int TotalProiecteFinalizate { get; set; }

    public List<AnuntModel> Anunturi { get; set; }
        = new();

    public List<OrderModel> ProiecteFinalizate { get; set; }
        = new();
    public double AverageRating { get; set; }

    public int TotalReviews { get; set; }

    public List<ReviewModel> Reviews { get; set; }
        = new();
}