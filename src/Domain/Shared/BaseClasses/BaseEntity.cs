namespace Domain.Shared.BaseClasses;

public abstract class BaseEntity
{
    public long Id { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.Now;
}