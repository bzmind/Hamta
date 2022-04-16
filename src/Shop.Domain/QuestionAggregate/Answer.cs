using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class Answer : BaseEntity
{
    public long ParentId { get; private set; }
    public string Description { get; private set; }
    public AnswerStatus Status { get; private set; }
    public enum AnswerStatus { Pending, Accepted, Rejected }
    public Answer(long parentId, string description)
    {
        Guard(description);
        ParentId = parentId;
        Description = description;
        Status = AnswerStatus.Pending;
    }

    public void SetStatus(AnswerStatus status)
    {
        Status = status;
    }

    public void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}