public class ReviewDto
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public string Comentariu { get; set; } = "";

    public string ReviewerName { get; set; } = "";

    public DateTime CreatedAt { get; set; }
}