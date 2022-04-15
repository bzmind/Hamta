using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.QuestionAggregate;

public class ProductQuestion : BaseAggregateRoot
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Description { get; private set; }

    private readonly List<ProductAnswer> _answers = new List<ProductAnswer>();
    public ReadOnlyCollection<ProductAnswer> Answers => _answers.AsReadOnly();

    public ProductQuestion(long productId, long customerId, string description)
    {
        Guard(description);
        ProductId = productId;
        CustomerId = customerId;
        Description = description;
    }

    public void AddAnswer(ProductAnswer answer)
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

    private void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}