using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class Reply : BaseEntity
{
    public long ParentId { get; private set; }
    public string Description { get; private set; }
    public ReplyStatus Status { get; private set; }

    public enum ReplyStatus { Pending, Accepted, Rejected }

    public Reply(long parentId, string description)
    {
        Guard(description);
        ParentId = parentId;
        Description = description;
        Status = ReplyStatus.Pending;
    }

    public void SetStatus(ReplyStatus status)
    {
        Status = status;
    }

    public void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}