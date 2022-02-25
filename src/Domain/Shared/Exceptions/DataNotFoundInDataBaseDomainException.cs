using Domain.Shared.BaseClasses;

namespace Domain.Shared.Exceptions;

public class DataNotFoundInDataBaseDomainException : BaseDomainException
{
    public DataNotFoundInDataBaseDomainException()
    {
        
    }

    public DataNotFoundInDataBaseDomainException(string message) : base(message)
    {

    }
}