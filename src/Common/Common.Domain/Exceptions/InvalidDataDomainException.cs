using Common.Domain.BaseClasses;

namespace Common.Domain.Exceptions;

public class InvalidDataDomainException : BaseDomainException
{
    public InvalidDataDomainException()
    {
        
    }

    public InvalidDataDomainException(string message) : base(message)
    {
        
    }
}