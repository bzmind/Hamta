using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Entities._DTOs;

namespace Shop.Query.Entities.Sliders.GetById;

public record GetSliderByIdQuery(long SliderId) : IBaseQuery<SliderDto?>;

public class GetSliderByIdQueryHandler : IBaseQueryHandler<GetSliderByIdQuery, SliderDto?>
{
    private readonly ShopContext _context;

    public GetSliderByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SliderDto?> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
    {
        var slider = await _context.Sliders
            .FirstOrDefaultAsync(f => f.Id == request.SliderId, cancellationToken);
        if (slider == null)
            return null;

        return new SliderDto
        {
            Id = slider.Id,
            CreationDate = slider.CreationDate,
            Image = slider.Image,
            Link = slider.Link,
            Title = slider.Title
        };
    }
}