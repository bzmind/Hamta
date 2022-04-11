using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CommentAggregate;

public class Comment : BaseAggregateRoot
{
    public long CustomerId { get; private set; }
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    private List<string> _positivePoints = new List<string>();
    public ReadOnlyCollection<string> PositivePoints => _positivePoints.AsReadOnly();

    private List<string> _negativePoints = new List<string>();
    public ReadOnlyCollection<string> NegativePoints => _negativePoints.AsReadOnly();
    public CommentStatus Status { get; private set; }
    public CommentRecommendation Recommendation { get; private set; }
    public int Likes { get => UsersWhoLiked.Count; private set {} }
    public int Dislikes { get => UsersWhoDisliked.Count; private set {} }
    private List<long> UsersWhoLiked { get; set; }
    private List<long> UsersWhoDisliked { get; set; }

    public enum CommentRecommendation { Neutral, Positive, Negative }
    public enum CommentStatus { Pending, Accepted, Rejected }

    public Comment(long productId, long customerId, string title, string description,
        CommentRecommendation recommendation)
    {
        Guard(title, description);
        ProductId = productId;
        CustomerId = customerId;
        Title = title;
        Description = description;
        Status = CommentStatus.Pending;
        Recommendation = recommendation;
        Likes = 0;
        Dislikes = 0;
        UsersWhoLiked = new List<long>();
        UsersWhoDisliked = new List<long>();
    }

    public void SetPositivePoints(List<string> positivePoints)
    {
        ValidateCommentPoints(positivePoints, nameof(positivePoints));
        _positivePoints = positivePoints;
    }

    public void SetNegativePoints(List<string> negativePoints)
    {
        ValidateCommentPoints(negativePoints, nameof(negativePoints));
        _negativePoints = negativePoints;
    }

    public void SetCommentStatus(CommentStatus status)
    {
        Status = status;
    }

    public void SetLikes(long customerId)
    {
        var userExists = UsersWhoLiked.Any(u => u == customerId);

        if (userExists)
        {
            UsersWhoLiked.Remove(customerId);
            Likes--;
        }

        UsersWhoLiked.Add(customerId);
        Likes++;
    }

    public void SetDislikes(long customerId)
    {
        var userExists = UsersWhoDisliked.Any(u => u == customerId);

        if (userExists)
        {
            UsersWhoDisliked.Remove(customerId);
            Dislikes--;
        }

        UsersWhoDisliked.Add(customerId);
        Dislikes++;
    }

    private void Guard(string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }

    private void ValidateCommentPoints(List<string>? points, string fieldName)
    {
        if (points == null)
            throw new NullOrEmptyDataDomainException($"{fieldName} is null");

        foreach (var point in points)
        {
            NullOrEmptyDataDomainException.CheckString(point, nameof(point));
        }

        if (points.Count > 5)
            throw new OutOfRangeValueDomainException($"{fieldName} count is more than limit");
    }
}