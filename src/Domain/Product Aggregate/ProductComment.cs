namespace Domain.Product_Aggregate;

public class ProductComment
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<string>? PositivePoints { get; private set; }
    public List<string>? NegativePoints { get; private set; }
    public CommentStatus Status { get; private set; }
    public CommentRecommendation Recommendation { get; private set; }

    public enum CommentRecommendation
    {
        نظری_ندارم,
        این_محصول_را_پیشنهاد_میکنم,
        این_محصول_را_پیشنهاد_نمیکنم
    }

    public enum CommentStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}