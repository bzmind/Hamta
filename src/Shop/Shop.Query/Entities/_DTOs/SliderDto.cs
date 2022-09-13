using Common.Query.BaseClasses;

namespace Shop.Query.Entities._DTOs;

public class SliderDto : BaseDto
{
    public string Title { get; set; }
    public string Link { get; set; }
    public string Image { get; set; }
}