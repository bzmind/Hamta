using Common.Domain.BaseClasses;

namespace Common.Domain.Exceptions;

public class DataNotFoundInDataBaseDomainException : BaseDomainException
{
    public DataNotFoundInDataBaseDomainException()
    {
        
    }

    public DataNotFoundInDataBaseDomainException(string message) : base(message)
    {

    }
}