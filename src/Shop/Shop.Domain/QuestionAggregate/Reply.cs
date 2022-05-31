using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class Reply : BaseEntity
{
    public long QuestionId { get; private set; }
    public long ProductId { get; private set; }
    public long UserId { get; private set; }
    public string Description { get; private set; }
    public string Status { get; private set; }

    public enum ReplyStatus { Pending, Accepted, Rejected }

    public Reply(long questionId, long productId, long userId, string description)
    {
        Guard(description);
        QuestionId = questionId;
        ProductId = productId;
        UserId = userId;
        Description = description;
        Status = ReplyStatus.Pending.ToString();
    }

    public void SetStatus(ReplyStatus status)
    {
        Status = status.ToString();
    }

    private void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}