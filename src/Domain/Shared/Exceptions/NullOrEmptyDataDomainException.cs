using Domain.Shared.BaseClasses;

namespace Domain.Shared.Exceptions;

public class NullOrEmptyDataDomainException : BaseDomainException
{
    public NullOrEmptyDataDomainException()
    {
        
    }

    public NullOrEmptyDataDomainException(string message) : base(message)
    {
        
    }

    public static void CheckString(string? data, string fieldName)
    {
        if (string.IsNullOrEmpty(data))
            throw new NullOrEmptyDataDomainException($"{fieldName} is null or empty");
    }

    public static void CheckData<T>(T? data, string fieldName) where T : BaseEntity
    {
        if (data == null)
            throw new NullOrEmptyDataDomainException($"The {fieldName} is null");
    }
}