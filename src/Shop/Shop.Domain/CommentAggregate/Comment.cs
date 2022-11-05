using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CommentAggregate;

public class Comment : BaseAggregateRoot
{
    public long UserId { get; private set; }
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public CommentStatus Status { get; private set; }
    public CommentRecommendation Recommendation { get; private set; }
    public int Likes { get; private set; }
    public int Dislikes { get; private set; }
    public int Score { get; set; }

    private readonly List<CommentPoint> _commentPoints = new();
    public IEnumerable<CommentPoint> CommentPoints => _commentPoints.ToList();

    private readonly List<CommentReaction> _commentReactions = new();
    public IEnumerable<CommentReaction> CommentReactions => _commentReactions.ToList();

    public enum CommentRecommendation
    {
        مطمئن_نیستم,
        پیشنهاد_میکنم,
        پیشنهاد_نمیکنم
    }

    public enum CommentStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    private Comment()
    {

    }

    public Comment(long productId, long userId, string title, string description, int score,
        CommentRecommendation recommendation)
    {
        Guard(title, description, score);
        ProductId = productId;
        UserId = userId;
        Title = title;
        Description = description;
        Status = CommentStatus.Pending;
        Recommendation = recommendation;
        Likes = 0;
        Dislikes = 0;
    }

    public void SetPositivePoints(List<string> positivePoints)
    {
        ValidateCommentPoints(positivePoints, nameof(positivePoints));

        var commentPoints = new List<CommentPoint>();
        positivePoints.ForEach(positivePoint =>
        {
            commentPoints.Add(new CommentPoint(Id, CommentPoint.PointStatus.Positive, positivePoint));
        });
        _commentPoints.AddRange(commentPoints);

        if (_commentPoints.Count > 20)
            throw new OperationNotAllowedDomainException("Comment points can't be more than 20");
    }

    public void SetNegativePoints(List<string> negativePoints)
    {
        ValidateCommentPoints(negativePoints, nameof(negativePoints));

        var commentPoints = new List<CommentPoint>();
        negativePoints.ForEach(negativePoint =>
        {
            commentPoints.Add(new CommentPoint(Id, CommentPoint.PointStatus.Negative, negativePoint));
        });
        _commentPoints.AddRange(commentPoints);

        if (_commentPoints.Count > 20)
            throw new OperationNotAllowedDomainException("Comment points can't be more than 20");
    }

    public void SetCommentStatus(CommentStatus status)
    {
        Status = status;
    }

    public void SetLikes(long userId)
    {
        var user = CommentReactions.FirstOrDefault(reaction => reaction.UserId == userId);

        if (user != null)
        {
            _commentReactions.Remove(user);

            if (user.Reaction == CommentReaction.ReactionType.Like)
            {
                Likes--;
            }
            else
            {
                _commentReactions.Add(new CommentReaction(Id, userId,
                    CommentReaction.ReactionType.Like));
                Dislikes--;
                Likes++;
            }

            return;
        }

        _commentReactions.Add(new CommentReaction(Id, userId,
            CommentReaction.ReactionType.Like));
        Likes++;
    }

    public void SetDislikes(long userId)
    {
        var user = CommentReactions.FirstOrDefault(c => c.UserId == userId);

        if (user != null)
        {
            _commentReactions.Remove(user);

            if (user.Reaction == CommentReaction.ReactionType.Dislike)
            {
                Dislikes--;
            }
            else
            {
                _commentReactions.Add(new CommentReaction(Id, userId,
                    CommentReaction.ReactionType.Dislike));
                Likes--;
                Dislikes++;
            }
            return;
        }

        _commentReactions.Add(new CommentReaction(Id, userId,
            CommentReaction.ReactionType.Dislike));
        Dislikes++;
    }

    private void Guard(string title, string description, int score)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
        OutOfRangeValueDomainException.CheckRange(0, 5, score, nameof(score));
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