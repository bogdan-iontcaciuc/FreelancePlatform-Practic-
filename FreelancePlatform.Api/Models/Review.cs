public class Review
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order? Order { get; set; }

    public int ReviewerId { get; set; }

    public User? Reviewer { get; set; }

    public int ReviewedUserId { get; set; }

    public User? ReviewedUser { get; set; }

    public int Rating { get; set; }

    public string Comentariu { get; set; } = "";

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;
}