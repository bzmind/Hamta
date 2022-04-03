namespace Common.Domain.Exceptions;

public class SlugAlreadyExistsDomainException : BaseDomainException
{
    public SlugAlreadyExistsDomainException()
    {
        
    }

    public SlugAlreadyExistsDomainException(string message) : base(message)
    {
        
    }
}