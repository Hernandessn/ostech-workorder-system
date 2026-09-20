namespace OSTech.Domain.Exceptions;

public class InvalidWorkOrderStatusException : DomainException
{
    public InvalidWorkOrderStatusException(string message) : base(message) { }
}
