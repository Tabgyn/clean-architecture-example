namespace CleanArchitectureExample.Domain.Common.Exceptions;

public sealed class InvalidEmailException : DomainException
{
    public InvalidEmailException()
        : base("Invalid e-mail")
    {
    }

    public InvalidEmailException(string message)
        : base(message)
    {
    }

    public InvalidEmailException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
