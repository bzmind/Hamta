using Common.Domain.BaseClasses;

namespace Common.Domain.Exceptions;

public class OutOfRangeValueDomainException : BaseDomainException
{
    public OutOfRangeValueDomainException()
    {
        
    }

    public OutOfRangeValueDomainException(string message) : base(message)
    {

    }

    public static void CheckRange(int min, int max, float value, string fieldName)
    {
        if (value < min)
            throw new OutOfRangeValueDomainException($"{fieldName} is less than minimum: " +
                                                     $"{fieldName} was {value}, " +
                                                     $"minimum was {min}");

        if (value > max)
            throw new OutOfRangeValueDomainException($"{fieldName} is greater than maximum: " +
                                                     $"{fieldName} was {value}, " +
                                                     $"maximum was {max}");
    }
}