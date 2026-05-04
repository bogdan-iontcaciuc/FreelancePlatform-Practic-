namespace FreelancePlatform.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ClientId { get; set; }
        public string Status { get; set; }
    }
}