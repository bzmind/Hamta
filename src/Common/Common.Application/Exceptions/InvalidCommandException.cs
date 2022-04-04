namespace Common.Application.Exceptions;

public class InvalidCommandException : Exception
{
    public InvalidCommandException() : base()
    {
        
    }

    public InvalidCommandException(string message) : base(message)
    {
        
    }
}