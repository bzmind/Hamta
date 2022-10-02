using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CommentAggregate;

public class CommentPoint : BaseEntity
{
    public long CommentId { get; private set; }
    public PointStatus Status { get; private set; }
    public string Description { get; private set; }

    public enum PointStatus
    {
        Positive,
        Negative
    }

    private CommentPoint()
    {
        
    }

    public CommentPoint(long commentId, PointStatus status, string description)
    {
        Guard(description);
        CommentId = commentId;
        Status = status;
        Description = description;
    }

    private void Guard(string point)
    {
        NullOrEmptyDataDomainException.CheckString(point, nameof(point));
    }
}