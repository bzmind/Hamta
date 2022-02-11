namespace Domain.Comment_Aggregate;

public class Comment
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<string>? PositivePoints { get; private set; }
    public List<string>? NegativePoints { get; private set; }
    public CommentStatus Status { get; private set; }
    public CommentState State { get; private set; }

    public enum CommentState
    {
        Neutral,
        Positive,
        Negative
    }

    public enum CommentStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}