using Common.Query.BaseClasses;
using Shop.Domain.Entities;

namespace Shop.Query.Entities._DTOs;

public class BannerDto : BaseDto
{
    public string Link { get; set; }
    public string Image { get; set; }
    public Banner.BannerPosition Position { get; set; }
}