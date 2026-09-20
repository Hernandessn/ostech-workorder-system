namespace OSTech.Domain.Exceptions;

public class CategoryDeletionBlockedException : DomainException
{
    public CategoryDeletionBlockedException(string message) : base(message) { }
}
