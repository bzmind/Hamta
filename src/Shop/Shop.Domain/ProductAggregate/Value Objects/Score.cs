﻿using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.ProductAggregate.Value_Objects;

public class Score : BaseValueObject
{
    public float Value { get; private set; }

    private const int MinimumScore = 0;
    private const int MaximumScore = 5;
    
    private Score()
    {

    }

    public Score(float scoreValue)
    {
        Guard(scoreValue);
        Value = scoreValue;
    }

    private void Guard(float scoreValue)
    {
        OutOfRangeValueDomainException.CheckRange(MinimumScore, MaximumScore, scoreValue, nameof(scoreValue));
    }
}