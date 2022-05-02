using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class Question : BaseAggregateRoot
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Description { get; private set; }

    private readonly List<Reply> _replies = new List<Reply>();
    public ReadOnlyCollection<Reply> Replies => _replies.AsReadOnly();
    public QuestionStatus Status { get; private set; }

    public enum QuestionStatus { Pending, Accepted, Rejected }

    public Question(long productId, long customerId, string description)
    {
        Guard(description);
        ProductId = productId;
        CustomerId = customerId;
        Description = description;
        Status = QuestionStatus.Pending;
    }

    public void AddReply(Reply reply)
    {
        _replies.Add(reply);
    }

    public void RemoveReply(long replyId)
    {
        var reply = Replies.FirstOrDefault(a => a.Id == replyId);

        if (reply == null)
            throw new NullOrEmptyDataDomainException("Reply was not found for this question");

        _replies.Remove(reply);
    }

    public void SetQuestionStatus(QuestionStatus status)
    {
        Status = status;
    }

    public void SetReplyStatus(long replyId, Reply.ReplyStatus replyStatus)
    {
        var reply = Replies.FirstOrDefault(a => a.Id == replyId);

        if (reply == null)
            throw new NullOrEmptyDataDomainException("Reply was not found for this question");

        reply.SetStatus(replyStatus);
    }

    private void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}