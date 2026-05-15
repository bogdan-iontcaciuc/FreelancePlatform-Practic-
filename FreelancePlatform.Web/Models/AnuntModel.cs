public class AnuntModel
{
    public int Id { get; set; }

    public string Titlu { get; set; } = "";

    public string Descriere { get; set; } = "";

    public string Categorie { get; set; } = "";

    public string TipAnunt { get; set; } = "";

    public string Tehnologii { get; set; } = "";

    public decimal PretSauBuget { get; set; }

    public DateTime DataPublicarii { get; set; }

    public int UtilizatorId { get; set; }

    public string? NumeUtilizator { get; set; }
}