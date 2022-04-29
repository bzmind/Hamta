using Shop.Domain.ColorAggregate;
using Shop.Query.Colors._DTOs;

namespace Shop.Query.Colors._Mappers;

internal static class ColorMapper
{
    public static ColorDto MapToColorDto(this Color? color)
    {
        if (color == null)
            return null;

        return new ColorDto
        {
            Id = color.Id,
            CreationDate = color.CreationDate,
            Name = color.Name,
            Code = color.Code
        };
    }

    public static List<ColorDto> MapToColorDto(this List<Color> colors)
    {
        var dtoColors = new List<ColorDto>();

        colors.ForEach(color =>
        {
            dtoColors.Add(new ColorDto
            {
                Id = color.Id,
                CreationDate = color.CreationDate,
                Name = color.Name,
                Code = color.Code
            });
        });

        return dtoColors;
    }
}