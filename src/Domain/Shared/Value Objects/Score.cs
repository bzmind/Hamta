using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;

namespace Domain.Shared.Value_Objects;

public class Score : BaseValueObject
{
    public float Value { get; private set; }

    private const int MinimumScore = 0;
    private const int MaximumScore = 5;
    
    public Score(float scoreValue)
    {
        OutOfRangeValueDomainException.CheckRange(MinimumScore, MaximumScore, scoreValue, nameof(scoreValue));
        Value = scoreValue;
    }
}