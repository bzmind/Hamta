namespace Common.Domain.BaseClasses;

public abstract class BaseEntity
{
    public long Id { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.Now;
}