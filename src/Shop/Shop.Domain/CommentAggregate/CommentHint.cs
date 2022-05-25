using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CommentAggregate;

public class CommentHint : BaseEntity
{
    public long CommentId { get; private set; }
    public string Status { get; private set; }
    public string Hint { get; private set; }

    public enum HintStatus
    {
        Positive,
        Negative
    }

    private CommentHint()
    {
        
    }

    public CommentHint(long commentId, HintStatus status, string hint)
    {
        Guard(hint);
        CommentId = commentId;
        Status = status.ToString();
        Hint = hint;
    }

    private void Guard(string hint)
    {
        NullOrEmptyDataDomainException.CheckString(hint, nameof(hint));
    }
}