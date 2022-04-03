namespace Common.Domain.Exceptions;

public class OperationNotAllowedDomainException : BaseDomainException
{
    public OperationNotAllowedDomainException()
    {
        
    }

    public OperationNotAllowedDomainException(string message) : base(message)
    {
        
    }
}