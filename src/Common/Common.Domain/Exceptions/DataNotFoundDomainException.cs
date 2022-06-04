namespace Common.Domain.Exceptions;

public class DataNotFoundDomainException : BaseDomainException
{
    public DataNotFoundDomainException()
    {
        
    }

    public DataNotFoundDomainException(string message) : base(message)
    {

    }
}