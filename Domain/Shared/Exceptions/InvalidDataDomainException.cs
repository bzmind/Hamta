using Domain.Shared.BaseClasses;

namespace Domain.Shared.Exceptions;

public class InvalidDataDomainException : BaseDomainException
{
    public InvalidDataDomainException()
    {
        
    }

    public InvalidDataDomainException(string message) : base(message)
    {
        
    }
}