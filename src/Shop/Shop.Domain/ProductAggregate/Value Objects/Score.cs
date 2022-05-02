using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.ProductAggregate.Value_Objects;

public class Score : BaseValueObject
{
    public int Value { get; private set; }

    private const int MinimumScore = 0;
    private const int MaximumScore = 5;
    
    private Score()
    {

    }

    public Score(int scoreValue)
    {
        Guard(scoreValue);
        Value = scoreValue;
    }

    private void Guard(int scoreValue)
    {
        OutOfRangeValueDomainException.CheckRange(MinimumScore, MaximumScore, scoreValue, nameof(scoreValue));
    }
}