using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.AvatarAggregate;

public class Avatar : BaseAggregateRoot
{
    public string Name { get; set; }
    public AvatarGender Gender { get; set; }

    public enum AvatarGender
    {
        Male = 1,
        Female = 2
    }

    public Avatar(string name, AvatarGender gender)
    {
        Guard(name);
        Name = name;
        Gender = gender;
    }

    private void Guard(string name)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
    }
}