using Microsoft.EntityFrameworkCore;

public class ReviewService
{
    private readonly AppDbContext _context;

    public ReviewService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)>
        CreateReview(
        int userId,
        CreateReviewRequest request)
    {
        if (request.Rating < 1 || request.Rating > 5)
        {
            return (false, "Rating invalid.");
        }

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);

        if (order == null)
        {
            return (false, "Order inexistent.");
        }

        if (order.Status != "Completed")
        {
            return (false, "Poți lăsa review doar după finalizarea comenzii.");
        }

        var isPartOfOrder =
            order.BuyerId == userId ||
            order.FreelancerId == userId;

        if (!isPartOfOrder)
        {
            return (false, "Nu ai acces.");
        }

        var alreadyReviewed = await _context.Reviews
            .AnyAsync(r =>
                r.OrderId == request.OrderId &&
                r.ReviewerId == userId);

        if (alreadyReviewed)
        {
            return (false, "Ai lăsat deja un review.");
        }

        var reviewedUserId =
            order.BuyerId == userId
                ? order.FreelancerId
                : order.BuyerId;

        var review = new Review
        {
            OrderId = request.OrderId,

            ReviewerId = userId,

            ReviewedUserId = reviewedUserId,

            Rating = request.Rating,

            Comentariu = request.Comentariu
        };

        _context.Reviews.Add(review);

        await _context.SaveChangesAsync();

        return (true, "Review adăugat.");
    }

    public async Task<List<ReviewDto>>
        GetReviewsForUser(int userId)
    {
        return await _context.Reviews
            .Include(r => r.Reviewer)
            .Where(r => r.ReviewedUserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewDto
            {
                Id = r.Id,

                Rating = r.Rating,

                Comentariu = r.Comentariu,

                ReviewerName = r.Reviewer != null
                    ? r.Reviewer.NumeComplet
                    : "Utilizator",

                CreatedAt = r.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<bool> HasUserReviewed(
        int orderId,
        int userId)
    {
        return await _context.Reviews
            .AnyAsync(r =>
                r.OrderId == orderId &&
                r.ReviewerId == userId);
    }
}