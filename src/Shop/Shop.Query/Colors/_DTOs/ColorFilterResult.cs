using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Colors._DTOs;

public class ColorFilterResult : BaseFilterResult<ColorDto, ColorFilterParam>
{
    
}

public class ColorFilterParam : BaseFilterParam
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}