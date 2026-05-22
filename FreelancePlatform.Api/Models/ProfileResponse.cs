public class ProfileResponse
{
    public int Id { get; set; }

    public string NumeComplet { get; set; } = "";

    public string Email { get; set; } = "";

    public string Descriere { get; set; } = "";

    public int TotalAnunturi { get; set; }

    public int TotalAplicatii { get; set; }

    public int TotalProiecteFinalizate { get; set; }

    public List<AnuntResponse> Anunturi { get; set; }
        = new();

    public List<OrderDto> ProiecteFinalizate { get; set; }
        = new();
}