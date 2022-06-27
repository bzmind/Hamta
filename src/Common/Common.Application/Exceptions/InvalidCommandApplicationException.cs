namespace Common.Application.Exceptions;

public class InvalidCommandApplicationException : Exception
{
    public InvalidCommandApplicationException() : base()
    {

    }

    public InvalidCommandApplicationException(string message) : base(message)
    {

    }
}