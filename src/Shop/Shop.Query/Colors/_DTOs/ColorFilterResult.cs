using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Colors._DTOs;

public class ColorFilterResult : BaseFilterResult<ColorDto, ColorFilterParams>
{
    
}

public class ColorFilterParams : BaseFilterParams
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}