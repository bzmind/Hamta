using Common.Query.BaseClasses;

namespace Shop.Query.Colors._DTOs;

public class ColorDto : BaseDto
{
    public string Name { get; set; }
    public string Code { get; set; }
}