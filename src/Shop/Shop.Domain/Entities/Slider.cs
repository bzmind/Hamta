using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.Entities;

public class Slider : BaseAggregateRoot
{
    public string Title { get; private set; }
    public string Link { get; private set; }
    public string Image { get; private set; }

    public Slider(string title, string link, string image)
    {
        Guard(title, link, image);
        Title = title;
        Link = link;
        Image = image;
    }

    public void Edit(string title, string link, string imageName)
    {
        Guard(title, link, imageName);
        Title = title;
        Link = link;
        Image = imageName;
    }

    public void Guard(string title, string link, string imageName)
    {
        NullOrEmptyDataDomainException.CheckString(link, nameof(link));
        NullOrEmptyDataDomainException.CheckString(imageName, nameof(imageName));
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
    }
}