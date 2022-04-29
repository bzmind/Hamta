using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Comments._DTOs;
using Shop.Query.Comments._Mappers;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQuery : BaseFilterQuery<CommentFilterResult, CommentFilterParams>
{
    public GetCommentByFilterQuery(CommentFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetCommentListQueryHandler : IBaseQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetCommentListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Comments
            .Include(c => c.CommentHints).OrderByDescending(c => c.CreationDate).AsQueryable();

        if (@params.CustomerId != null)
            query = query.Where(c => c.CustomerId == @params.CustomerId);

        if (@params.ProductId != null)
            query = query.Where(c => c.ProductId == @params.ProductId);

        if (@params.Status != null)
            query = query.Where(c => c.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        return new CommentFilterResult
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Join(
                    _shopContext.Customers,
                    comment => comment.CustomerId,
                    customer => customer.Id,
                    (comment, customer) => comment.MapToCommentDto(customer))
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}