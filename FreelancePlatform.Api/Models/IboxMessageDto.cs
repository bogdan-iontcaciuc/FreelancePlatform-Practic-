namespace FreelancePlatform.Api.Models;

public class InboxMessageDto
{
    public int Id { get; set; }

    public string Continut { get; set; } = string.Empty;

    public string Expeditor { get; set; } = string.Empty;

    public string TitluAnunt { get; set; } = string.Empty;

    public DateTime DataTrimiterii { get; set; }
}