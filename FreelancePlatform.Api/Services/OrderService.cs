using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> CreateFromApplication(
        int applicationId,
        int ownerId)
    {
        var application = await _context.Applications
            .Include(a => a.Anunt)
            .FirstOrDefaultAsync(a => a.Id == applicationId);

        if (application == null)
        {
            return (false, "Aplicația nu există.");
        }

        if (application.Anunt == null)
        {
            return (false, "Anunț invalid.");
        }

        if (application.Anunt.UtilizatorId != ownerId)
        {
            return (false, "Nu ai acces.");
        }
        var existingOrder = await _context.Orders
    .AnyAsync(o => o.AnuntId == application.AnuntId
        && o.FreelancerId == application.FreelancerId);

        if (existingOrder)
        {
            return (false, "Order deja existent.");
        }
        application.Status = "Accepted";

        application.Anunt.Status = "În lucru";

        var order = new Order
        {
            AnuntId = application.AnuntId,
            BuyerId = ownerId,
            FreelancerId = application.FreelancerId,
            Status = "InProgress"
        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return (true, "Order creat cu succes.");
    }

    public async Task<List<OrderDto>> GetOrders(int userId)
    {
        return await _context.Orders
            .Include(o => o.Anunt)
            .Where(o =>
                o.BuyerId == userId ||
                o.FreelancerId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderDto
            {
                Id = o.Id,


AnuntId = o.AnuntId,

                TitluAnunt = o.Anunt != null
    ? o.Anunt.Titlu
    : "Anunț",

                Categorie = o.Anunt != null
    ? o.Anunt.Categorie
    : "",

                TipAnunt = o.Anunt != null
    ? o.Anunt.TipAnunt
    : "",

                PretSauBuget = o.Anunt != null
    ? o.Anunt.PretSauBuget
    : 0,

                Status = o.Status,

                CreatedAt = o.CreatedAt


})

            .ToListAsync();
    }

    public async Task<(bool Success, string Message)> CompleteOrder(
        int orderId,
        int userId)
    {
        var order = await _context.Orders
            .Include(o => o.Anunt)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
        {
            return (false, "Order inexistent.");
        }

        if (order.BuyerId != userId)
        {
            return (false, "Nu ai acces.");
        }
        order.Status = "Completed";
        order.CompletedAt = DateTime.UtcNow;

        if (order.Anunt != null)
        {
            order.Anunt.Status = "Inactiv";
        }

        await _context.SaveChangesAsync();

        return (true, "Order finalizat.");
    }
}