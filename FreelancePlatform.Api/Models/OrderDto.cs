public class OrderDto
{
    public int Id { get; set; }

public int AnuntId { get; set; }

    public string TitluAnunt { get; set; } = "";

    public string Categorie { get; set; } = "";

    public string TipAnunt { get; set; } = "";

    public decimal PretSauBuget { get; set; }

    public string Status { get; set; } = "";

    public DateTime CreatedAt { get; set; }


}
