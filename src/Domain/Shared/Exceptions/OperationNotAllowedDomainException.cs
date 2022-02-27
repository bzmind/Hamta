using Domain.Shared.BaseClasses;

namespace Domain.Shared.Exceptions;

public class OperationNotAllowedDomainException : BaseDomainException
{
    public OperationNotAllowedDomainException()
    {
        
    }

    public OperationNotAllowedDomainException(string message) : base(message)
    {
        
    }
}