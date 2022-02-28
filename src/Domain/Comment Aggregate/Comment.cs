using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;

namespace Domain.Comment_Aggregate;

public class Comment : BaseAggregateRoot
{
    public long CustomerId { get; private set; }
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<string> PositivePoints { get; private set; }
    public List<string> NegativePoints { get; private set; }
    public CommentStatus Status { get; private set; }
    public CommentRecommendation Recommendation { get; private set; }
    public int Likes { get; private set; }
    public int Dislikes { get; private set; }

    public enum CommentRecommendation { Neutral, Positive, Negative }
    public enum CommentStatus { Pending, Accepted, Rejected }

    public Comment(long productId, long customerId, string title, string description,
        CommentRecommendation recommendation)
    {
        Validate(title, description);
        ProductId = productId;
        CustomerId = customerId;
        Title = title;
        Description = description;
        PositivePoints = new List<string>();
        NegativePoints = new List<string>();
        Status = CommentStatus.Pending;
        Recommendation = recommendation;
        Likes = 0;
        Dislikes = 0;
    }

    public void SetPositivePoints(List<string> positivePoints)
    {
        ValidateCommentPoints(positivePoints, nameof(positivePoints));
        PositivePoints = positivePoints;
    }

    public void SetNegativePoints(List<string> negativePoints)
    {
        ValidateCommentPoints(negativePoints, nameof(negativePoints));
        NegativePoints = negativePoints;
    }

    public void SetCommentStatus(CommentStatus status)
    {
        Status = status;
    }

    public void IncreaseLikes() => Likes++;

    public void DecreaseLikes() => Likes--;

    public void IncreaseDislikes() => Dislikes++;

    public void DecreaseDislikes() => Dislikes--;

    private void Validate(string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }

    private void ValidateCommentPoints(List<string>? points, string fieldName)
    {
        if (points == null)
            throw new NullOrEmptyDataDomainException($"{fieldName} is null");

        if (points.Count > 5)
            throw new OutOfRangeValueDomainException($"{fieldName} count is more than limit");
    }
}