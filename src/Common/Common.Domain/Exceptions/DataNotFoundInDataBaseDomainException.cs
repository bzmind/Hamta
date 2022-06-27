using Common.Domain.BaseClasses;

namespace Common.Domain.Exceptions;

public class DataNotFoundInDatabaseDomainException : BaseDomainException
{
    public DataNotFoundInDatabaseDomainException()
    {

    }

    public DataNotFoundInDatabaseDomainException(string message) : base(message)
    {

    }
}