using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class Question : BaseAggregateRoot
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Description { get; private set; }

    private readonly List<Answer> _answers = new List<Answer>();
    public ReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();
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

    public void AddAnswer(Answer answer)
    {
        _answers.Add(answer);
    }

    public void RemoveAnswer(long answerId)
    {
        var answer = Answers.FirstOrDefault(a => a.Id == answerId);

        if (answer == null)
            throw new NullOrEmptyDataDomainException("Answer was not found for this question");

        _answers.Remove(answer);
    }

    public void SetQuestionStatus(QuestionStatus status)
    {
        Status = status;
    }

    public void SetAnswerStatus(long answerId, Answer.AnswerStatus answerStatus)
    {
        var answer = Answers.FirstOrDefault(a => a.Id == answerId);

        if (answer == null)
            throw new NullOrEmptyDataDomainException("Answer was not found for this question");

        answer.SetStatus(answerStatus);
    }

    private void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}