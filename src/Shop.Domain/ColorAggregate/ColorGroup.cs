using System.Collections.ObjectModel;
using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;

namespace Shop.Domain.ColorAggregate;

public class ColorGroup : BaseAggregateRoot
{
    public string Name { get; private set; }

    private readonly List<Color> _colors = new List<Color>();
    public ReadOnlyCollection<Color> Colors => _colors.AsReadOnly();

    public ColorGroup(string name)
    {
        Guard(name);
        Name = name;
    }

    public void AddColor(string name, string code)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        NullOrEmptyDataDomainException.CheckString(code, nameof(code));

        _colors.Add(new Color(Id, name, code));
    }

    public void RemoveColor(long colorId)
    {
        var color = _colors.FirstOrDefault(c => c.Id == colorId);

        if (color == null)
            throw new NullOrEmptyDataDomainException("Color not found in this color group");

        _colors.Remove(color);
    }

    private void Guard(string name)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
    }
}