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
    public string Status { get; private set; }
    public string Recommendation { get; private set; }
    public int Likes { get; private set; }
    public int Dislikes { get; private set; }

    private readonly List<CommentReaction> _commentReactions = new List<CommentReaction>();
    public IEnumerable<CommentReaction> CommentReactions => _commentReactions.ToList();

    public enum CommentRecommendation { Neutral, Positive, Negative }
    public enum CommentStatus { Pending, Accepted, Rejected }

    private Comment()
    {

    }

    public Comment(long productId, long customerId, string title, string description,
        CommentRecommendation recommendation)
    {
        Guard(title, description);
        ProductId = productId;
        CustomerId = customerId;
        Title = title;
        Description = description;
        Status = CommentStatus.Pending.ToString();
        Recommendation = recommendation.ToString();
        Likes = 0;
        Dislikes = 0;
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
        Status = status.ToString();
    }

    public void SetLikes(long customerId)
    {
        var customer = CommentReactions.FirstOrDefault(c => c.CustomerId == customerId);

        if (customer != null)
        {
            _commentReactions.Remove(customer);

            if (customer.Reaction == CommentReaction.ReactionType.Like.ToString())
            {
                Likes--;
            }
            else
            {
                _commentReactions.Add(new CommentReaction(Id, customerId,
                    CommentReaction.ReactionType.Like));
                Dislikes--;
                Likes++;
            }

            return;
        }

        _commentReactions.Add(new CommentReaction(Id, customerId,
            CommentReaction.ReactionType.Like));
        Likes++;
    }

    public void SetDislikes(long customerId)
    {
        var customer = CommentReactions.FirstOrDefault(c => c.CustomerId == customerId);

        if (customer != null)
        {
            _commentReactions.Remove(customer);

            if (customer.Reaction == CommentReaction.ReactionType.Dislike.ToString())
            {
                Dislikes--;
            }
            else
            {
                _commentReactions.Add(new CommentReaction(Id, customerId,
                    CommentReaction.ReactionType.Dislike));
                Likes--;
                Dislikes++;
            }
            return;
        }

        _commentReactions.Add(new CommentReaction(Id, customerId,
            CommentReaction.ReactionType.Dislike));
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