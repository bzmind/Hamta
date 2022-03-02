using Domain.Shared.BaseClasses;

namespace Domain.Shared.Exceptions;

public class SlugAlreadyExistsDomainException : BaseDomainException
{
    public SlugAlreadyExistsDomainException()
    {
        
    }

    public SlugAlreadyExistsDomainException(string message) : base(message)
    {
        
    }
}