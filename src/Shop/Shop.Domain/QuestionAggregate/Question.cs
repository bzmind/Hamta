using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class Question : BaseAggregateRoot
{
    public long ProductId { get; private set; }
    public long UserId { get; private set; }
    public string Description { get; private set; }
    public string Status { get; private set; }

    private readonly List<Reply> _replies = new List<Reply>();
    public IEnumerable<Reply> Replies => _replies.ToList();

    public enum QuestionStatus { Pending, Accepted, Rejected }

    public Question(long userId, long productId, string description)
    {
        Guard(description);
        ProductId = productId;
        UserId = userId;
        Description = description;
        Status = QuestionStatus.Pending.ToString();
    }

    public void AddReply(long userId, string description)
    {
        Guard(description);
        _replies.Add(new Reply(Id, ProductId, userId, description));
    }

    public void RemoveReply(long replyId)
    {
        var reply = _replies.FirstOrDefault(r => r.Id == replyId);

        if (reply == null)
            throw new InvalidDataDomainException("Reply not found");

        _replies.Remove(reply);
    }

    public void SetStatus(QuestionStatus status)
    {
        Status = status.ToString();
    }

    private void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}