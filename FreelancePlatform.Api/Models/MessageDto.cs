public class MessageDto
{
    public int Id { get; set; }

    public int ExpeditorId { get; set; }

    public string Expeditor { get; set; } = "";

    public string Continut { get; set; } = "";

    public bool EsteLivrare { get; set; }

    public string? FisierUrl { get; set; }

    public DateTime DataTrimiterii { get; set; }
    public bool EsteEditat { get; set; }

    public DateTime? DataEditarii { get; set; }
}