using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.Entities;

public class Banner : BaseAggregateRoot
{
    public string Link { get; private set; }
    public string Image { get; private set; }
    public BannerPosition Position { get; private set; }

    public enum BannerPosition
    {
        وسط_صفحه,
        بالای_اسلایدر,
        پایین_اسلایدر,
        سمت_چپ_اسلایدر,
        سمت_راست_اسلایدر
    }

    public Banner(string link, string image, BannerPosition position)
    {
        Guard(link, image);
        Link = link;
        Image = image;
        Position = position;
    }

    public void Edit(string link, string imageName, BannerPosition position)
    {
        Guard(link, imageName);
        Link = link;
        Image = imageName;
        Position = position;
    }

    public void Guard(string link, string imageName)
    {
        NullOrEmptyDataDomainException.CheckString(link, nameof(link));
        NullOrEmptyDataDomainException.CheckString(imageName, nameof(imageName));
    }
}