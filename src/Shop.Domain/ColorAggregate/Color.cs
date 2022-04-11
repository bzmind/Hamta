using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;

namespace Shop.Domain.ColorAggregate;

public class Color : BaseEntity
{
    public long GroupId { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }

    public Color(long groupId, string name, string code)
    {
        Guard(name, code);
        GroupId = groupId;
        Name = name;
        Code = code;
    }

    private void Guard(string name, string code)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        NullOrEmptyDataDomainException.CheckString(code, nameof(code));
    }
}