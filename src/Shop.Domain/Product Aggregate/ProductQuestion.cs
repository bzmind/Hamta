using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;

namespace Shop.Domain.Product_Aggregate;

public class ProductQuestion : BaseEntity
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Description { get; private set; }
    public List<ProductAnswer> Answers { get; private set; }

    public ProductQuestion(long productId, long customerId, string description, List<ProductAnswer> answers)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
        ProductId = productId;
        CustomerId = customerId;
        Description = description;
        Answers = answers;
    }

    public void AddAnswer(ProductAnswer answer)
    {
        Answers.Add(answer);
    }

    public void RemoveAnswer(long answerId)
    {
        var answer = Answers.FirstOrDefault(a => a.Id == answerId);

        if (answer == null)
            throw new NullOrEmptyDataDomainException($"No such answer was found for this question: {answerId}");

        Answers.Remove(answer);
    }
}