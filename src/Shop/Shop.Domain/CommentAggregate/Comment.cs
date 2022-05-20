using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CommentAggregate;

public class Comment : BaseAggregateRoot
{
    public long CustomerId { get; private set; }
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    private readonly List<CommentHint> _commentHints = new List<CommentHint>();
    public IEnumerable<CommentHint> CommentHints => _commentHints.ToList();
    public CommentStatus Status { get; private set; }
    public CommentRecommendation Recommendation { get; private set; }
    public int Likes { get => UsersWhoLiked.Count; private set {} }
    public int Dislikes { get => UsersWhoDisliked.Count; private set {} }

    public enum CommentRecommendation { Neutral, Positive, Negative }
    public enum CommentStatus { Pending, Accepted, Rejected }

    private List<long> UsersWhoLiked { get; set; }
    private List<long> UsersWhoDisliked { get; set; }

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

    public void SetPositivePoints(List<string> positiveHints)
    {
        ValidateCommentPoints(positiveHints, nameof(positiveHints));

        var commentHints = new List<CommentHint>();
        positiveHints.ForEach(positiveHint =>
        {
            commentHints.Add(new CommentHint(Id, CommentHint.HintStatus.Positive, positiveHint));
        });
        _commentHints.AddRange(commentHints);

        if (_commentHints.Count > 20)
            throw new OperationNotAllowedDomainException("Comment hints can't be more than 20");
    }

    public void SetNegativePoints(List<string> negativeHints)
    {
        ValidateCommentPoints(negativeHints, nameof(negativeHints));

        var commentHints = new List<CommentHint>();
        negativeHints.ForEach(negativeHint =>
        {
            commentHints.Add(new CommentHint(Id, CommentHint.HintStatus.Negative, negativeHint));
        });
        _commentHints.AddRange(commentHints);

        if (_commentHints.Count > 20)
            throw new OperationNotAllowedDomainException("Comment hints can't be more than 20");
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

        if (points.Count > 10)
            throw new OutOfRangeValueDomainException($"{fieldName} count is more than limit");
    }
}