namespace Domain.Shared.BaseClasses;

public class BaseDomainException : Exception
{
    public BaseDomainException()
    {
        
    }

    public BaseDomainException(string message) : base(message)
    {
        
    }
}