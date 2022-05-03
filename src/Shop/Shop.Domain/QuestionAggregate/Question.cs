using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class Question : BaseAggregateRoot
{
    public long? ParentQuestionId { get; private set; }
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Description { get; private set; }
    public QuestionStatus Status { get; private set; }

    public enum QuestionStatus { Pending, Accepted, Rejected }

    public Question(long productId, long customerId, string description, long? parentQuestionId = null)
    {
        Guard(description);
        ProductId = productId;
        CustomerId = customerId;
        Description = description;
        ParentQuestionId = parentQuestionId;
        Status = QuestionStatus.Pending;
    }

    public void SetStatus(QuestionStatus status)
    {
        Status = status;
    }

    private void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}