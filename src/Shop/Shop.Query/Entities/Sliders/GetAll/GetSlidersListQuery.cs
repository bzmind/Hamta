using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Entities._DTOs;

namespace Shop.Query.Entities.Sliders.GetAll;

public record GetSlidersListQuery : IBaseQuery<List<SliderDto>>;

public class GetSlidersListQueryHandler : IBaseQueryHandler<GetSlidersListQuery, List<SliderDto>>
{
    private readonly ShopContext _context;

    public GetSlidersListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<SliderDto>> Handle(GetSlidersListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sliders.OrderByDescending(d => d.Id)
            .Select(slider => new SliderDto
            {
                Id = slider.Id,
                CreationDate = slider.CreationDate,
                Image = slider.Image,
                Link = slider.Link,
                Title = slider.Title
            }).ToListAsync(cancellationToken);
    }
}