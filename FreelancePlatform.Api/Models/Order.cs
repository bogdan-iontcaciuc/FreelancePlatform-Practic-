using FreelancePlatform.Api.Models;

public class Order
{
    public int Id { get; set; }

    public int AnuntId { get; set; }

    public Anunt? Anunt { get; set; }

    public int BuyerId { get; set; }

    public User? Buyer { get; set; }

    public int FreelancerId { get; set; }

    public User? Freelancer { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public DateTime? DeliveredAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public ICollection<Message> Messages { get; set; }
        = new List<Message>();
    public ICollection<Review> Reviews { get; set; }
    = new List<Review>();
}