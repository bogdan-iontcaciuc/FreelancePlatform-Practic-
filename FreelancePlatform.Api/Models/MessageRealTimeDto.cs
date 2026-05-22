namespace FreelancePlatform.Api.Models;

public class MessageRealtimeDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ExpeditorId { get; set; }

    public string Expeditor { get; set; } = "";

    public string Continut { get; set; } = "";

    public bool EsteLivrare { get; set; }

    public string? FisierUrl { get; set; }

    public bool IsSeen { get; set; }

    public DateTime DataTrimiterii { get; set; }
}