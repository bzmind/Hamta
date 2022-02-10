using Domain.Shared.Exceptions;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Exceptions;

public class OutOfRangeValueDomainExceptionTests
{
    [Fact]
    public void CheckRange_should_throw_OutOfRangeValueDomainException_when_value_is_less_than_minimum()
    {
        int value = -1;
        int min = 0;
        int max = 5;
        string fieldName = "test";

        var result = () => OutOfRangeValueDomainException.CheckRange(min, max, value, fieldName);

        result.Should().ThrowExactly<OutOfRangeValueDomainException>()
            .WithMessage($"{fieldName} is less than minimum: {fieldName} was {value}, minimum was {min}");
    }

    [Fact]
    public void CheckRange_should_throw_OutOfRangeValueDomainException_when_value_is_greater_than_maximum()
    {
        int value = 10;
        int min = 0;
        int max = 5;
        string fieldName = "test";
        
        var result = () => OutOfRangeValueDomainException.CheckRange(min, max, value, fieldName);

        result.Should().ThrowExactly<OutOfRangeValueDomainException>()
            .WithMessage($"{fieldName} is greater than maximum: {fieldName} was {value}, maximum was {max}");
    }
}