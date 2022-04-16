using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.ColorAggregate;

public class Color : BaseAggregateRoot
{
    public string Name { get; private set; }
    public string Code { get; private set; }

    public Color(string name, string code)
    {
        Guard(name, code);
        Name = name;
        Code = code;
    }

    public void Edit(string name, string code)
    {
        Guard(name, code);
        Name = name;
        Code = code;
    }

    private void Guard(string name, string code)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        NullOrEmptyDataDomainException.CheckString(code, nameof(code));
    }
}